using System.Linq;
using Settings;
using UnityEngine;

namespace Managers
{
    public static class HeaderController
    {
        private static Animator _header;
    
        public static void InitHeader()
        {
            var headerObject = SettingsList.Get<UIElementsSettings>().elementsList.First(v => v.elementName == UIElementName.Header).gameObject;

            _header = GameObject.Instantiate(headerObject, UIManager.canvas).GetComponent<Animator>();
        }

        public static void ShowHeader()
        {
            _header.SetTrigger("Show");
        }

        public static void HideHeader()
        {
            _header.SetTrigger("Hide");
        }
    }
}

