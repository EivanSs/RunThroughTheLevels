using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Init
{
    public class MainSceneInit : MonoBehaviour
    {
        public RectTransform canvasTransform;
    
        public RectTransform screensParent;

        public void Awake()
        {
            QualitySettings.SetQualityLevel(3, true);
            
            UIManager.Init(canvasTransform, screensParent);
        
            ScreensManager.OpenScreen(ScreenName.MainScreen, ScreenTransitionType.MoveUp);

            HeaderController.InitHeader();
        }
    }
}

