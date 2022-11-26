using System.Collections.Generic;
using Settings;

namespace UI
{
    public interface IStoreElement
    {
        public string GetTitle();
    
        public string GetDescription();

        public int GetLevel();

        public PurchaseSettings PurchaseSettings();

        public InfoBarSettings InfoBarSettings();

        public ItemState GetItemState(int forLevel);
    }
}
