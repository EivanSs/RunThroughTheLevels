using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/WeaponTypes/PistolSettings")]
    public class PistolSettingsList : ScriptableObject
    {
        [SerializeField] private List<PistolSettings> _pistolSettings;
        
        public PistolSettings GetSettings(string guid)
        {
            return _pistolSettings.FirstOrDefault(v => v.pistolGuid == guid);
        }

        public List<PistolSettings> GetAll()
        {
            var pistolSettings = new List<PistolSettings>();

            foreach (var settings in _pistolSettings)
                pistolSettings.Add(settings);

            return pistolSettings;
        }
    
        [Serializable]
        public struct PistolSettings
        {
            public string pistolName;
            public string pistolGuid;
        
            public float damage;
            public float accuracy;
            public float rateOfFire;

            public AudioClip shotSound;
        }
    }
}

