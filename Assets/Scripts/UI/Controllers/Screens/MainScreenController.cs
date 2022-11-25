using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Controllers.Screens
{
    public class MainScreenController : BaseScreen
    {
        public Button boutiqueButton;
        public Button storeButton;
        public Button playButton;

        public void Awake()
        {
            boutiqueButton.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.BoutiqueScreen, ScreenTransitionType.MoveLeft);
            });
            
            storeButton.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.StoreScreen, ScreenTransitionType.MoveDown);
            });
        
            playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(2);
            });
        }
    }
}


