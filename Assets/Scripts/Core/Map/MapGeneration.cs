using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using Managers;
using Settings;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Map
{
    public class MapGeneration : MonoBehaviour
    {
        [SerializeField] private Transform _focusTransform;
        
        private ListQueue<(Identifier, ISpawnObject)> _spawnObjectsQueue = new ();
        
        private List<WorldObject> _spawnedObjects = new ();

        private List<Identifier> _registeredPoints = new ();

        private void Awake()
        {
            StartCoroutine(ObjectsCreating());
            StartCoroutine(MapCreation());
        }

        IEnumerator ObjectsCreating()
        {
            while (true)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (_spawnObjectsQueue.Count > 0)
                    {
                        var spawnObject = _spawnObjectsQueue.Dequeue();
                
                        var spawnedGameObject = spawnObject.Item2.Spawn();

                        var worldObject = new WorldObject(spawnedGameObject, spawnObject.Item1);
                
                        _spawnedObjects.Add(worldObject);
                    }
                }
                
                yield return new WaitForSeconds(.1f);
            }
        }
        
        IEnumerator MapCreation()
        {
            while (true)
            {
                var registeredObjectsToRemove = new List<Identifier>();
                
                foreach (var registeredPoint in _registeredPoints)
                {
                    bool needful = IdentifierInArea(registeredPoint);

                    if (!needful)
                    {
                        // Remove form Queue
                        
                        var objectsToRemove = _spawnObjectsQueue
                            .FindAll(v => v.Item1 == registeredPoint);

                        foreach (var removeObject in objectsToRemove)
                            _spawnObjectsQueue.Remove(removeObject);
                        
                        // Destroy Objects

                        var objectsToDestroy = _spawnedObjects
                            .FindAll(v => v.Match(registeredPoint));

                        foreach (var objectToDestroy in objectsToDestroy)
                        {
                            _spawnedObjects.Remove(objectToDestroy);
                            
                            objectToDestroy.Destroy();
                        }
                        
                        // Add to destroy registered point
                        
                        registeredObjectsToRemove.Add(registeredPoint);
                    }
                }

                foreach (var removeObject in registeredObjectsToRemove)
                    _registeredPoints.Remove(removeObject);
                
                ReviseAround(identifier =>
                {
                    var registered = _registeredPoints.Any(v => v == identifier);
                    
                    if (registered)
                        return;
                    
                    Vector2 position = new Vector2(identifier.x, identifier.y);

                    var quadPrefab = SettingsList.Get<MapGenerationSettings>().levels[0].quad;

                    var quad = new QuadObject(quadPrefab, position);

                    _spawnObjectsQueue.Enqueue((identifier, quad));

                    _registeredPoints.Add(identifier);
                });
                
                yield return new WaitForSeconds(1f);
            }
        }

        private void ReviseAround(Action<Identifier> action)
        {
            for (int aroundX = 0; aroundX < 50; aroundX++)
            {
                for (int aroundY = 0; aroundY < 30; aroundY++)
                {
                    var x = _focusTransform.position.x + aroundX - 25;
                    var y = _focusTransform.position.z + aroundY - 15;
                        
                    var identifier = new Identifier((int)x, (int)y);
                    
                    action.Invoke(identifier);
                }
            }
        }

        private bool IdentifierInArea(Identifier identifier)
        {
            
            if (identifier.x - _focusTransform.position.x > 25)
                return false;
            
            if (identifier.x - _focusTransform.position.x < -25)
                return false;
            
            if (identifier.y - _focusTransform.position.z > 15)
                return false;
            
            if (identifier.y - _focusTransform.position.z < -15)
                return false;

            return true;
        }
        
        private struct WorldObject
        {
            private Identifier identifier;
            
            private GameObject _gameObject;

            public WorldObject(GameObject gameObject, Identifier identifier)
            {
                this.identifier = identifier;
                _gameObject = gameObject;
            }

            public void Destroy()
            {
                Object.Destroy(_gameObject);
            }
            public bool Match(Identifier identifier)
            {
                return this.identifier == identifier;
            }
        }

        private struct Identifier
        {
            public int x;
            public int y;

            public Identifier(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            #region Operators
            public static bool operator == (Identifier a, Identifier b)
            {
                return a.x == b.x && a.y == b.y;
            }
            
            public static bool operator != (Identifier a, Identifier b)
            {
                return a.x != b.x || a.y != b.y;
            }
            #endregion
        }
    }
}

