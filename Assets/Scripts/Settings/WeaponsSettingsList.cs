using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/WeaponsSettings")]
    public class WeaponsSettingsList : ScriptableObject
    {
        public PistolSettingsList pistolSettingsList;
    
        [SerializeField] private List<WeaponSettings> _weaponSettings;

        public WeaponSettings GetSettings(string guid)
        {
            return _weaponSettings.FirstOrDefault(v => v.guid == guid);
        }

        public List<WeaponSettings> GetAll()
        {
            var weaponSettings = new List<WeaponSettings>();

            foreach (var settings in _weaponSettings)
                weaponSettings.Add(settings);

            return weaponSettings;
        }

        public float GetMaxDamageFromLevel(int level)
        {
            float maxValue = 0;
            
            foreach (var pistolSetting in pistolSettingsList.GetAll())
            {
                var weaponLevel = GetSettings(pistolSetting.pistolGuid).level;
                
                if (weaponLevel != level)
                    continue;

                maxValue = Math.Max(maxValue, pistolSetting.damage);
            }

            return maxValue;
        }

        [Serializable]
        public struct WeaponSettings
        {
            public string name;
            public string description;
        
            public int level;

            public WeaponType weaponType;

            public PurchaseSettings purchaseSettings;

            public string guid;
        }
    }
}

