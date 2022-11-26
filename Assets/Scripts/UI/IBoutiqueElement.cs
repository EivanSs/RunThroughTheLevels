using System.Collections.Generic;
using Settings;

namespace UI
{
    public interface IBoutiqueElement
    {
        public string GetTitle();
    
        public string GetDescription();

        public int GetLevel();

        public PurchaseSettings PurchaseSettings();

        public InfoBarSettings InfoBarSettings();

        public ItemState GetItemState(int forLevel);
    }

    public static class PurchaseUtils
    {
        public static ItemState GetDefaultItemState(int itemLevel, PurchaseSettings itemPurchaseSettings, int forLevel)
        {
            bool onlyCrystals = itemPurchaseSettings.moneys == 0;

            bool blockedBuyLevel = itemLevel > forLevel;

            bool purchased = false;

            var itemState = ItemState.Buy;

            if (itemPurchaseSettings.isPremiumItem)
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
