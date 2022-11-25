using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/UIElementsSettings")]
    public class UIElementsSettings : ScriptableObject
    {
        public List<UIElementSetting> elementsList;
    
        [Serializable]
        public struct UIElementSetting
        {
            public GameObject gameObject;

            public UIElementName elementName;
        }
    }

    public enum UIElementName
    {
        Header,
        InfoBarPanel
    }
}