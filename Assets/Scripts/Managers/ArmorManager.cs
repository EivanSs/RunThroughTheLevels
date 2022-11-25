using System.Collections;
using System.Collections.Generic;
using Core;
using Settings;
using UnityEngine;


namespace Managers
{
    public static class ArmorManager
    {
        private static List<Armor> _armors = new ();
        
        public static List<Armor> armors => _armors;

        public static void Initialize()
        {
            var armorSettingsList = SettingsList.Get<ArmorSettingsList>();
            
            foreach (var armorSettings in armorSettingsList.GetAll())
            {
                var weapon = new Armor(armorSettings.guid);
                
                _armors.Add(weapon);
            }
        }
    }
}
