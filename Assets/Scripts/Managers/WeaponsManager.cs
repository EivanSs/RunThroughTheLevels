using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Settings;
using UnityEngine;

namespace Managers
{
    public static class WeaponsManager
    {
        private static List<Weapon> _weapons = new ();
        
        public static List<Weapon> weapons => _weapons;

        public static void Initialize()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>();
            
            foreach (var weaponSettings in weaponsSettings.GetAll())
            {
                var weapon = new Weapon(weaponSettings.guid);
                
                _weapons.Add(weapon);
            }
        }
    }
}
    
