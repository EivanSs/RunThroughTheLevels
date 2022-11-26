using Settings;
using UI;

namespace Core
{
    public class Armor : IBoutiqueElement
    {
        private readonly string _armorGuid;

        public Armor(string armorGuid)
        {
            _armorGuid = armorGuid;
        }
    
        public string GetTitle()
        {
            var armorSettings = SettingsList.Get<ArmorSettingsList>();

            return armorSettings.GetSettings(_armorGuid).name;
        }

        public string GetDescription()
        {
            return "";
        }
    
        public int GetLevel()
        {
            var armorSettings = SettingsList.Get<ArmorSettingsList>();

            return armorSettings.GetSettings(_armorGuid).level;
        }

        public PurchaseSettings PurchaseSettings()
        {
            var armorSettings = SettingsList.Get<ArmorSettingsList>();

            return armorSettings.GetSettings(_armorGuid).purchaseSettings;
        }
    
        public InfoBarSettings InfoBarSettings()
        {
            var shopInfoBarSettings = new InfoBarSettings
            {
                withParameters = false
            };
        
            return shopInfoBarSettings;
        }

        public ItemState GetItemState(int forLevel)
        {
            return PurchaseUtils.GetDefaultItemState(GetLevel(), PurchaseSettings(), forLevel);
        }
    }

}
