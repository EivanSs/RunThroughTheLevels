using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Settings;
using UI;
using Unity.Mathematics;
using UnityEngine;

namespace Core
{
    public class Weapon : IBoutiqueElement
    {
        public readonly string weaponGuid;

        public Weapon(string weaponGuid)
        {
            this.weaponGuid = weaponGuid;
        }
        
        public string GetTitle()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(weaponGuid);

            return weaponsSettings.name;
        }

        public string GetDescription()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(weaponGuid);

            return weaponsSettings.description;
        }
        
        public int GetLevel()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(weaponGuid);

            return weaponsSettings.level;
        }

        public PurchaseSettings PurchaseSettings()
        {
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(weaponGuid);

            return weaponsSettings.purchaseSettings;
        }

        public ShopInfoBarSettings ShopInfoBarSettings()
        {
            var parameters = new List<InfoBarParameter>();
            
            var weaponsSettings = SettingsList.Get<WeaponsSettingsList>().GetSettings(weaponGuid);

            switch (weaponsSettings.weaponType)
            {
                case WeaponType.Pistol:
                    var pistolSettings = SettingsList.Get<WeaponsSettingsList>().pistolSettingsList.GetSettings(weaponGuid);

                    var damageParameter = new InfoBarParameter();
                    
                    damageParameter.parameterType = InfoBarParameterType.Damage;
                    damageParameter.normalizedValue = pistolSettings.damage / 
                                                      SettingsList.Get<WeaponsSettingsList>().GetMaxDamageFromLevel(weaponsSettings.level);
                    damageParameter.textValue = pistolSettings.damage.ToString();
                    
                    parameters.Add(damageParameter);
                    
                    var accuracyParameter = new InfoBarParameter();
                    
                    accuracyParameter.parameterType = InfoBarParameterType.Accuracy;
                    accuracyParameter.normalizedValue = pistolSettings.accuracy / 100;
                    accuracyParameter.textValue = $"{pistolSettings.accuracy}%";
                    
                    parameters.Add(accuracyParameter);
                    
                    var rateOfFireParameter = new InfoBarParameter();
                    
                    rateOfFireParameter.parameterType = InfoBarParameterType.RateOfFire;
                    rateOfFireParameter.normalizedValue = 1 - 1 / pistolSettings.rateOfFire;
                    rateOfFireParameter.textValue = $"{pistolSettings.rateOfFire}S";
                    
                    parameters.Add(rateOfFireParameter);
                    break;
            }

            var shopInfoBarSettings = new ShopInfoBarSettings
            {
                withParameters = true,
                parameters = parameters
            };

            return shopInfoBarSettings;
        }
        
        public ItemState GetItemState(int forLevel)
        {
            ItemState itemState;
            
            bool onlyCrystals = PurchaseSettings().moneys == 0;

            bool blockedBuyLevel = GetLevel() > forLevel;

            bool purchased = weaponGuid == "663";;

            itemState = ItemState.Buy;

            if (PurchaseSettings().isPremiumItem)
                itemState = ItemState.BlockedForPremium;
            
            if (blockedBuyLevel)
                itemState = ItemState.BlockedBuyLevel;
            
            if (purchased)
                itemState = ItemState.Purchased;

            if (itemState == ItemState.Buy && onlyCrystals)
                itemState = ItemState.BuyForCrystals;

            return itemState;
        }
    }

}
