using System.Collections;
using System.Collections.Generic;
using Settings;
using UI;
using UnityEngine;

namespace Core
{
    public class Armor : IBoutiqueElement
    {
        public string weaponGuid { get; }

        public Armor(string weaponGuid)
        {
            this.weaponGuid = weaponGuid;
        }
    
        public string GetTitle()
        {
            var armorSettings = SettingsList.Get<ArmorSettingsList>();

            return armorSettings.GetSettings(weaponGuid).name;
        }

        public string GetDescription()
        {
            return "";
        }
    
        public int GetLevel()
        {
            var armorSettings = SettingsList.Get<ArmorSettingsList>();

            return armorSettings.GetSettings(weaponGuid).level;
        }

        public PurchaseSettings PurchaseSettings()
        {
            var armorSettings = SettingsList.Get<ArmorSettingsList>();

            return armorSettings.GetSettings(weaponGuid).purchaseSettings;
        }
    
        public ShopInfoBarSettings ShopInfoBarSettings()
        {
            var shopInfoBarSettings = new ShopInfoBarSettings
            {
                withParameters = false
            };
        
            return shopInfoBarSettings;
        }

        public ItemState GetItemState(int forLevel)
        {
            ItemState itemState;
            
            bool onlyCrystals = PurchaseSettings().moneys == 0;

            bool blockedBuyLevel = GetLevel() > forLevel;

            bool purchased = false;

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
