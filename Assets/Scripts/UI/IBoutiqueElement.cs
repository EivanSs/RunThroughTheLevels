using System.Collections.Generic;
using Settings;
using UI;
using UI.Reusable;

namespace UI
{
    public interface IBoutiqueElement
    {
        public string GetTitle();
    
        public string GetDescription();

        public int GetLevel();

        public PurchaseSettings PurchaseSettings();

        public ShopInfoBarSettings ShopInfoBarSettings();

        public ItemState GetItemState(int forLevel);
    }
}
