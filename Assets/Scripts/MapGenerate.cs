using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using Utils;

public class MapGenerate : MonoBehaviour
{
	private Dictionary<int, Vector2> loadedPoints = new Dictionary<int, Vector2>();

	private ListQueue<ObjectSpawnInfo> mapObjectsQueue = new ListQueue<ObjectSpawnInfo>();

	private List<IndexedGameObject> spawnedObjects = new List<IndexedGameObject>();
	
	private List<IndexedGameObject> noChunkObjects = new List<IndexedGameObject>();

	private List<UniqueObject> destroyedObjects = new List<UniqueObject>();

	private MapGenerateSettings mapGenerateSettings;

	public static bool AllSpawned;

	private void Awake()
	{
		StartCoroutine(MapUpdating());
		StartCoroutine(ObjectsSpawning());
		StartCoroutine(NoChunkMovableDestroying());
		StartCoroutine(ObjectsTransformSynchronize());

		AllSpawned = false;
	}

	private void Update()
	{
		Vector3 playerPosition = gameObject.transform.position;
			
		int player64PosX = To64(playerPosition.x);
		int player64PosZ = To64(playerPosition.z);
		
		if (GetPointsAround(player64PosX, player64PosZ) != loadedPoints)
			UpdateMap();
	}

	IEnumerator ObjectsTransformSynchronize()
	{
		yield return new WaitForEndOfFrame();
		while (true)
		{
			foreach (var spawnedObject in spawnedObjects.Where(v => v.IsMovable))
			{
				spawnedObject.Position = spawnedObject.GameObject.transform.position;
				spawnedObject.Rotation = spawnedObject.GameObject.transform.rotation;
			}
			
			yield return new WaitForSecondsRealtime(0.25f);
		}
	}
	
	IEnumerator ObjectsSpawning()
	{
		yield return new WaitForEndOfFrame();
		while(true)
		{
			for (int i = 0; i < 15; i++)
			{
				if (mapObjectsQueue.Count == 0)
				{
					AllSpawned = true;
					continue;
				}

				ObjectSpawnInfo objectSpawnInfo = mapObjectsQueue.Dequeue();

				var position = new Vector3(objectSpawnInfo.uniqueObject.UniqueAddress.X * 64 + objectSpawnInfo.positionX - 32, objectSpawnInfo.yOffset, objectSpawnInfo.uniqueObject.UniqueAddress.Y * 64 + objectSpawnInfo.positionZ - 32);
				GameObject spawnedObject = Instantiate(objectSpawnInfo.prefab, position, Quaternion.Euler(0, objectSpawnInfo.yRotation, 0));
				
				if (objectSpawnInfo.scaleChanging) 
					spawnedObject.transform.localScale = new Vector3(objectSpawnInfo.scale, 1, objectSpawnInfo.scale);
				
				spawnedObjects.Add(new IndexedGameObject
				{
					UniqueObject = objectSpawnInfo.uniqueObject,
					GamePosition = objectSpawnInfo.gamePosition,
					IsMovable = objectSpawnInfo.isMovable,
					GameObject = spawnedObject,
				});
				
				objectSpawnInfo.onSpawned?.Invoke(spawnedObject, objectSpawnInfo.uniqueObject, objectSpawnInfo.gamePosition);
			}
			
			yield return new WaitForSecondsRealtime(0.025f);
		}
	}
	
	IEnumerator MapUpdating()
	{
		yield return new WaitForEndOfFrame();
		while(true)
		{
			UpdateMap();
			
			yield return new WaitForSecondsRealtime(2f);
		}
	}
	
	IEnumerator NoChunkMovableDestroying()
	{
		yield return new WaitForEndOfFrame();
		while(true)
		{
			foreach (var indexedGameObject in noChunkObjects.Where(v => v.IsMovable))
			{
				if (Vector3.Distance(indexedGameObject.GameObject.transform.position, transform.position) > 32)
				{
					noChunkObjects.Remove(indexedGameObject);

					Destroy(indexedGameObject.GameObject);
					
					break;
				}
			}
			
			yield return new WaitForSecondsRealtime(0.5f);
		}
	}

	public void UpdateMap()
	{
		Vector3 playerPosition = gameObject.transform.position;
			
		int player64PosX = To64(playerPosition.x);
		int player64PosZ = To64(playerPosition.z);
		
		Dictionary<int, Vector2> pointsAround = GetPointsAround(player64PosX, player64PosZ);

		int chunksCleared = 0;
		
		foreach (var loadedPair in loadedPoints)
		{
			if (pointsAround.All(v => v.Value != loadedPair.Value))
			{
				ClearChunk((int)loadedPair.Value.x, (int)loadedPair.Value.y);
				chunksCleared++;
			}
		}
		
		foreach (var pair in pointsAround)
		{
			if (loadedPoints.All(v => v.Value != pair.Value))
				InitChunk((int)pair.Value.x, (int)pair.Value.y);
		}

		loadedPoints = pointsAround;
	}
	
	private void ClearChunk(int x, int y)
	{
		List<ObjectSpawnInfo> objectsToRemove = new List<ObjectSpawnInfo>();

		foreach (var mapObject in mapObjectsQueue.Where(v => v.uniqueObject.UniqueAddress.InChunk(x, y)))
			objectsToRemove.Add(mapObject);
		
		objectsToRemove.ForEach(v => mapObjectsQueue.Remove(v));
		
		List<IndexedGameObject> indexedObjectsToRemove = new List<IndexedGameObject>();
		
		foreach (var indexedGameObject in spawnedObjects.Where(v => v.UniqueObject.UniqueAddress.InChunk(x, y)))
		{
			if (indexedGameObject.IsMovable && Vector3.Distance(indexedGameObject.GameObject.transform.position, transform.position) <= 32)
				noChunkObjects.Add(indexedGameObject);
			else
				Destroy(indexedGameObject.GameObject);
			
			indexedObjectsToRemove.Add(indexedGameObject);
		}
		
		indexedObjectsToRemove.ForEach(v => spawnedObjects.Remove(v));
	}

	private void InitChunk(int x, int y)
	{
		System.Random Rand = new System.Random(x + y + PlayerPrefs.GetInt("GenerationKey"));

		UniqueList<FloatVector> matchedPointsPool = new UniqueList<FloatVector>();

		for (int i = 0; i < 100; i++)
			matchedPointsPool.Add(new FloatVector(Rand.Next(0, 64), Rand.Next(0, 64)), 
				(v1, v2) => !((int)v1.x == (int)v2.x && (int)v1.y == (int)v2.y));
		
		AllUtils.SpreadVectors(matchedPointsPool, 3, 5, 0.2f, 0, 64);

		matchedPointsPool.RemoveElements(matchedPointsPool.FindAll(v => v.x == 0 || v.y == 0));

		UniqueList<IntVector> freePointsPool = new UniqueList<IntVector>();

		for (int i = 0; i < 100; i++)
			freePointsPool.Add(new IntVector(Rand.Next(0, 64), Rand.Next(0, 64)), 
				(v1, v2) => !(v1.x == v2.x && v1.y == v2.y));
		
		var matches = ListManager.Match(freePointsPool, matchedPointsPool, 
			(v, v2) => v.x == (int)v2.x && v.y == (int)v2.y);
		
		freePointsPool.RemoveElements(matches);

		int level = 0;

		UniqueAddress defaultAddress = new UniqueAddress
		{
			X = x,
			Y = y
		};

		Chunk chunk = mapGenerateSettings.Chunks.FirstOrDefault(v => v.Level == level);
		
		if (chunk != null)
			mapObjectsQueue.Enqueue(new ObjectSpawnInfo
			{
				uniqueObject = new UniqueObject(GameObjectType.Chunk, defaultAddress),
				gamePosition = new GamePosition(0, 0),
				positionX = 32,
				positionZ = 32,
				prefab = chunk.Prefab,
			});

		foreach (var preset in mapGenerateSettings.Presets.Where(v => v.Level == level))
		{
			if (preset.Prefabs.Count == 0)
				continue;
			
			for (int i = 0; i < preset.Count; i++)
			{
				IntVector point = new IntVector(-1, -1);

				switch (preset.SpawnType)
				{
					case SpawnType.Matched:
						point = matchedPointsPool.PullOutRandom(Rand).ToInt();
						break;
					case SpawnType.Random:
						point = freePointsPool.PullOutRandom(Rand);
						break;
				}
				
				mapObjectsQueue.Enqueue(new ObjectSpawnInfo
				{
					uniqueObject = new UniqueObject(preset.Type, defaultAddress),
					gamePosition = new GamePosition(0, 0),
					positionX = point.x,
					positionZ = point.y,
					prefab = preset.Prefabs[Rand.Next(0, preset.Prefabs.Count)],
					yRotation = preset.RandomRotation ? Rand.Next(0, 4) * 90 : 0,
					scaleChanging = preset.ScaleChange.Change,
					scale = preset.ScaleChange.Change ? preset.ScaleChange.ScaleValue + Mathf.Lerp(-preset.ScaleChange.ScaleDifference, 
						preset.ScaleChange.ScaleDifference, Rand.Next(0, 100) / 100f) : 1,
					yOffset = preset.YOffset
				});
			}
		}
	}

	[Serializable]
	private class SavedGameObject
	{
		public Vector3 Position;
		public Quaternion Rotation;
		public UniqueObject UniqueObject;
		public GamePosition GamePosition;
		public float TimeForShot;
	}

	private class ObjectSpawnInfo
	{
		public UniqueObject uniqueObject;
		public GamePosition gamePosition;
		public float positionX;
		public float positionZ;
		public GameObject prefab;
		public float yRotation;
		public float scale;
		public bool scaleChanging;
		public float yOffset;
		public bool isMovable;
		public Action<GameObject, UniqueObject, GamePosition> onSpawned;

		public ObjectSpawnInfo()
		{
			scale = 1;
		}
	}
	
	private class IndexedGameObject
	{
		public UniqueObject UniqueObject;
		public GamePosition GamePosition;
		public bool IsMovable;
		public GameObject GameObject;
		public Vector3 Position;
		public Quaternion Rotation;
		public float TimeForShot;
	}

	private Dictionary<int, Vector2> GetPointsAround(int _64X, int _64Z) 
	{
		Dictionary<int, Vector2> matrix = new Dictionary<int, Vector2>();
		
		matrix.Add(0, new Vector2(_64X-1, _64Z-1));
		matrix.Add(1, new Vector2(_64X, _64Z-1));
		matrix.Add(2, new Vector2(_64X+1, _64Z-1));
		matrix.Add(3, new Vector2(_64X-1, _64Z));
		matrix.Add(4, new Vector2(_64X, _64Z));
		matrix.Add(5, new Vector2(_64X+1, _64Z));
		matrix.Add(6, new Vector2(_64X-1, _64Z+1));
		matrix.Add(7, new Vector2(_64X, _64Z+1));
		matrix.Add(8, new Vector2(_64X+1, _64Z+1));

		return matrix;
	}

	private int To64(float num)
	{
		if (num > 0) 
			num+=32;
		else 
			num-=32;
		num-=num%64f;
		num/=64f;
		return (int)num;
	}
}

[Serializable]
public class UniqueAddress
{
	public int X;
	public int Y;
	public int SpawnId;

	public bool InChunk(int x, int y)
	{
		return X == x && Y == y;
	}
	
	public static bool operator == (UniqueAddress uniqueAddress1, UniqueAddress uniqueAddress2)
	{
		return uniqueAddress1.X == uniqueAddress2.X && uniqueAddress1.Y == uniqueAddress2.Y && uniqueAddress1.SpawnId == uniqueAddress2.SpawnId;
	}
    
	public static bool operator != (UniqueAddress uniqueAddress1, UniqueAddress uniqueAddress2)
	{
		return uniqueAddress1.X != uniqueAddress2.X || uniqueAddress1.Y != uniqueAddress2.Y || uniqueAddress1.SpawnId != uniqueAddress2.SpawnId;
	}
}

[Serializable]
public class UniqueObject
{
	public GameObjectType Type;
	public UniqueAddress UniqueAddress;
	
	public UniqueObject(GameObjectType type, int x, int y, int spawnId = 0)
	{
		Type = type;
		UniqueAddress = new UniqueAddress
		{
			X = x,
			Y = y,
			SpawnId = spawnId
		};
	}
	
	public UniqueObject(GameObjectType type, UniqueAddress uniqueAddress)
	{
		Type = type;
		UniqueAddress = uniqueAddress;
	}
	
	public static bool operator == (UniqueObject uniqueObject1, UniqueObject uniqueObject2)
	{
		return uniqueObject1.Type == uniqueObject2.Type && uniqueObject1.UniqueAddress == uniqueObject2.UniqueAddress;
	}
    
	public static bool operator != (UniqueObject uniqueObject1, UniqueObject uniqueObject2)
	{
		return uniqueObject1.Type != uniqueObject2.Type || uniqueObject1.UniqueAddress != uniqueObject2.UniqueAddress;
	}
}

[Serializable]
public struct GamePosition
{
	public int Level;
	public int Id;

	public GamePosition(int level, int id)
	{
		Level = level;
		Id = id;
	}

	public static bool operator == (GamePosition gamePosition1, GamePosition gamePosition2)
	{
		return gamePosition1.Level == gamePosition2.Level && gamePosition1.Id == gamePosition2.Id;
	}
    
	public static bool operator != (GamePosition gamePosition1, GamePosition gamePosition2)
	{
		return gamePosition1.Level != gamePosition2.Level || gamePosition1.Id != gamePosition2.Id;
	}
}

public enum GameObjectType
{
	Chunk, Wall, Tree, Stone, Flower, Drop, Mob
}