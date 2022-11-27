using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MapGenerateSettings", menuName = "Settings/MapGenerateSettings", order = 0)]
public class MapGenerateSettings : ScriptableObject
{
    public List<Chunk> Chunks = new List<Chunk>();
    
    public List<Preset> Presets = new List<Preset>();

    public GameObject Wall;
}

[Serializable]
public class Chunk
{
    public GameObject Prefab;
    public int Level;
}

[Serializable]
public class Preset
{
    
    public List<GameObject> Prefabs;
    public int Count;
    
    public GameObjectType Type;

    public SpawnType SpawnType;
    public bool RandomRotation;
    public ScaleChanging ScaleChange;
    public float YOffset;
    
    public int Level;
}

public enum SpawnType
{
    Random,
    Matched
}

[Serializable]
public class ScaleChanging
{
    public bool Change;
    public float ScaleValue;
    public float ScaleDifference;
}