using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/MapGenerationSettings")]
    public class MapGenerationSettings : ScriptableObject
    {
        public List<LevelSettings> levels;

        [Serializable]
        public struct LevelSettings
        {
            public int level;
            public GameObject quad;
            public List<SpawnObject> presets;
        }

        [Serializable]
        public struct SpawnObject
        {
            public string name;
            public GameObject gameObject;
            public float chance;
        }
    }
}