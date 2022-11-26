using System.Collections.Generic;
using Settings;
using UI;

namespace Core
{
    public class Weapon : IBoutiqueElement
    {
        private readonly string _weaponGuid;

        public Weapon(string weaponGuid)
        {
            _weaponGuid = weaponGuid;
        }
        
        public string GetTitle()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(_weaponGuid);

            return weaponsSettings.name;
        }

        public string GetDescription()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(_weaponGuid);

            return weaponsSettings.description;
        }
        
        public int GetLevel()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(_weaponGuid);

            return weaponsSettings.level;
        }

        public PurchaseSettings PurchaseSettings()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(_weaponGuid);

            return weaponsSettings.purchaseSettings;
        }

        public InfoBarSettings InfoBarSettings()
        {
            List<InfoBarParameter> parameters;
            
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(_weaponGuid);

            switch (weaponsSettings.weaponType)
            {
                case WeaponType.Pistol:
                    var pistolSettings = SettingsList.Get<WeaponsSettingsList>().pistolSettingsList.GetSettings(_weaponGuid);

                    var damageParameter = new InfoBarParameter
                    {
                        parameterType = InfoBarParameterType.Damage,
                        normalizedValue = pistolSettings.damage / 
                                          SettingsList.Get<WeaponsSettingsList>().GetMaxDamageFromLevel(weaponsSettings.level),
                        textValue = pistolSettings.damage.ToString()
                    };
                    
                    var accuracyParameter = new InfoBarParameter
                    {
                        parameterType = InfoBarParameterType.Accuracy,
                        normalizedValue = pistolSettings.accuracy / 100,
                        textValue = $"{pistolSettings.accuracy}%"
                    };
                    
                    var rateOfFireParameter = new InfoBarParameter
                    {
                        parameterType = InfoBarParameterType.RateOfFire,
                        normalizedValue = 1 - 1 / pistolSettings.rateOfFire,
                        textValue = $"{pistolSettings.rateOfFire}S"
                    };

                    parameters = new List<InfoBarParameter> { damageParameter, accuracyParameter, rateOfFireParameter };
                    
                    break;
                default:
                    parameters = null;
                    
                    break;
            }

            var shopInfoBarSettings = new InfoBarSettings
            {
                withParameters = true,
                parameters = parameters
            };

            return shopInfoBarSettings;
        }
        
        public ItemState GetItemState(int forLevel)
        {
            return PurchaseUtils.GetDefaultItemState(GetLevel(), PurchaseSettings(), forLevel);
        }
    }

}
