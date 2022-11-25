using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Settings;
using UI.Controllers.Screens;
using UI.Reusable;
using UI.Reusable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Controllers.Screens
{
    public class StoreScreenController : BaseScreen
    {
        public GameObject storeItemPrefab;
        
        public Button buttonBack;
        
        public HorizontalButtonsBarController buttonsBar;

        public ShopItemsBarController shopItemsBarController;

        private SelectableScrollRect _selectableScrollRect => shopItemsBarController.GetComponent<SelectableScrollRect>();

        public override bool enableInfoBar
        {
            get
            {
                return true;
            }
        }
        
        public void Start()
        {
            buttonBack.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.MainScreen, ScreenTransitionType.MoveUp);
                
                HeaderController.ShowHeader();
            });
            
            List<(string, Action)> buttons = new List<(string, Action)>();

            var crystalsLots = SettingsList.Get<StoreSettings>().crystalsLots.Select(v => (object)v).ToList();

            buttons.Add(("Crystals", () =>
            {
                shopItemsBarController.Setup(crystalsLots, 
                    storeItemPrefab, 
                    (elementGameObject, elementObject) =>
                    {
                        var storeItemController = elementGameObject.GetComponent<StoreItemController>();
                        
                        storeItemController.Setup((CrystalsLotSettings)elementObject);
                    }, 
                    selectedObject =>
                    {
                        _infoBarController.UpdateInfo(this, InfoBarVariousType.Store, variantGameObject =>
                        {
                            var storeInfoBarController = variantGameObject.GetComponent<StoreInfoBarController>();
                            
                            storeInfoBarController.Init((CrystalsLotSettings)selectedObject);
                        });
                    },
                    0);
            }));

            buttons.Add(("Keys", () => { }));
            
            buttons.Add(("Premium", () => { }));
            
            buttons.Add(("XP Multipliers", () => { }));
            
            buttons.Add(("Chests", () => { }));

            buttons.Add(("No Ads", () => { }));

            buttonsBar.Init(buttons, 0);
        }
    }
}
