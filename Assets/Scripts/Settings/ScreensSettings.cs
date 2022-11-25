using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UI.Controllers.Screens;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/ScreensSettings")]
    public class ScreensSettings : ScriptableObject
    {
        public List<ScreenSetting> screensList;
    
        [Serializable]
        public struct ScreenSetting
        {
            public BaseScreen screen;

            public ScreenName screenName;
        }
    }
}
