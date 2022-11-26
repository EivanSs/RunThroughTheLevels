using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Controllers.Screens
{
    public class MainScreenController : BaseScreen
    {
        [SerializeField] private Button _boutiqueButton;
        [SerializeField] private Button _storeButton;
        [SerializeField] private Button _playButton;

        public void Awake()
        {
            _boutiqueButton.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.BoutiqueScreen, ScreenTransitionType.MoveLeft);
            });
            
            _storeButton.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.StoreScreen, ScreenTransitionType.MoveDown);
            });
        
            _playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(2);
            });
        }
    }
}


