using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/ArmorSettings")]
    public class ArmorSettingsList : ScriptableObject
    {
        [SerializeField] private List<ArmorSettings> _armorSettings;

        public ArmorSettings GetSettings(string guid)
        {
            return _armorSettings.First(v => v.guid == guid);
        }

        public List<ArmorSettings> GetAll()
        {
            var armorSettings = new List<ArmorSettings>();

            foreach (var settings in _armorSettings)
                armorSettings.Add(settings);

            return armorSettings;
        }
        
        [Serializable]
        public struct ArmorSettings
        {
            public string name;
            
            public int healthsIncrease;

            public int level;

            public ArmorType armorType;
            
            public PurchaseSettings purchaseSettings;

            public string guid;
        }
    }
}