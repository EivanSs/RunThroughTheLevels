using System;
using System.Linq;
using DG.Tweening;
using Settings;
using UI.Controllers.Screens;
using UI.Reusable;
using UnityEngine;

namespace Managers
{
    public static class ScreensManager
    {
        private const float transitionSpeed = 0.75f;
        
        private static BaseScreen _currentScreen;
        
        private static RectTransform _currentScreenTransform => _currentScreen.GetComponent<RectTransform>();

        private static InfoBarController _infoBar;

        public static void OpenScreen(ScreenName screenName, ScreenTransitionType transitionType)
        {
            var newScreenPrefab = SettingsList.Get<ScreensSettings>().screensList
                .First(v => v.screenName == screenName).screen;

            switch (transitionType)
            {
                case ScreenTransitionType.MoveUp:
                    if (_currentScreen != null)
                    {
                        var screenObject = _currentScreen.gameObject;
                            
                        _currentScreenTransform.DOLocalMoveY(GetScreenMaxHeightOffset(), transitionSpeed).onComplete += () =>
                        {
                            GameObject.Destroy(screenObject);
                        };
                    }

                    _currentScreen = GameObject.Instantiate(newScreenPrefab, UIManager.screensParent);

                    _currentScreenTransform.anchoredPosition = new Vector2(0, -GetScreenMaxHeightOffset());
                    
                    _currentScreenTransform.DOLocalMoveY(0, transitionSpeed);

                    break;
                
                case ScreenTransitionType.MoveDown:
                    if (_currentScreen != null)
                    {
                        var screenObject = _currentScreen.gameObject;
                            
                        _currentScreenTransform.DOLocalMoveY(-GetScreenMaxHeightOffset(), transitionSpeed).onComplete += () =>
                        {
                            GameObject.Destroy(screenObject);
                        };
                    }

                    _currentScreen = GameObject.Instantiate(newScreenPrefab, UIManager.screensParent);

                    _currentScreenTransform.anchoredPosition = new Vector2(0, GetScreenMaxHeightOffset());
                    
                    _currentScreenTransform.DOLocalMoveY(0, transitionSpeed);

                    break;
                
                case ScreenTransitionType.MoveLeft:
                    if (_currentScreen != null)
                    {
                        var screenObject = _currentScreen.gameObject;
                            
                        _currentScreenTransform.DOLocalMoveX(-GetScreenMaxWidthOffset(), transitionSpeed).onComplete += () =>
                        {
                            GameObject.Destroy(screenObject);
                        };
                    }

                    _currentScreen = GameObject.Instantiate(newScreenPrefab, UIManager.screensParent);

                    _currentScreenTransform.anchoredPosition = new Vector2(GetScreenMaxWidthOffset(), 0);
                    
                    _currentScreenTransform.DOLocalMoveX(0, transitionSpeed);

                    break;
                
                case ScreenTransitionType.MoveRight:
                    if (_currentScreen != null)
                    {
                        var screenObject = _currentScreen.gameObject;
                            
                        _currentScreenTransform.DOLocalMoveX(GetScreenMaxWidthOffset(), transitionSpeed).onComplete += () =>
                        {
                            GameObject.Destroy(screenObject);
                        };
                    }

                    _currentScreen = GameObject.Instantiate(newScreenPrefab, UIManager.screensParent);

                    _currentScreenTransform.anchoredPosition = new Vector2(-GetScreenMaxWidthOffset(), 0);
                    
                    _currentScreenTransform.DOLocalMoveX(0, transitionSpeed);
                    

                    break;
                
                default:
                    throw new Exception("Unidentified transition type");
            }
            
            if (_currentScreen.enableInfoBar)
            {
                if (_infoBar == null)
                {
                    var infoBarPanelPrefab = SettingsList.Get<UIElementsSettings>().elementsList
                        .First(v => v.elementName == UIElementName.InfoBarPanel).gameObject;

                    _infoBar = GameObject.Instantiate(infoBarPanelPrefab, UIManager.canvas)
                        .GetComponent<InfoBarController>();
                    
                    _infoBar.Show();
                }
                
                _currentScreen.SetupInfoBar(_infoBar);
                
                _infoBar.Init(_currentScreen);
            }
            else
            {
                if (_infoBar != null)
                {
                    _infoBar.HideAndDestroy();

                    _infoBar = null;
                }
            }

            void OnTransitionComplete()
            {
                
            }
        }

        private static float GetScreenMaxHeightOffset()
        {
            return (UIManager.canvas.rect.height + UIManager.screensParent.rect.height) / 2;
        }
        
        private static float GetScreenMaxWidthOffset()
        {
            return (UIManager.canvas.rect.width + UIManager.screensParent.rect.width) / 2;
        }
    }

    public enum ScreenName
    {
        MainScreen,
        BoutiqueScreen,
        UpgradeScreen,
        StoreScreen,
    }

    public enum ScreenTransitionType
    {
        Replace,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }
}