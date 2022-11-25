using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UI.Controllers.Screens;
using UI.Reusable;
using UI.Reusable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Controllers.Screens
{
    public class BoutiqueScreenController : BaseScreen
    {
        public GameObject boutiqueItemPrefab;
        
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
                ScreensManager.OpenScreen(ScreenName.MainScreen, ScreenTransitionType.MoveRight);
                
                HeaderController.ShowHeader();
            });
            
            List<(string, Action)> buttons = new List<(string, Action)>();

            List<object> weaponObjects = WeaponsManager.weapons.Select(v => (object)v).ToList();

            buttons.Add(("Weapons", () =>
            {
                shopItemsBarController.Setup(weaponObjects, 
                    boutiqueItemPrefab, 
                    (elementGameObject, elementObject) =>
                    {
                        var shopItemController = elementGameObject.GetComponent<BoutiqueItemController>();
                        
                        shopItemController.Setup((IBoutiqueElement)elementObject);
                    }, 
                    selectedObject =>
                    {
                        _infoBarController.UpdateInfo(this, InfoBarVariousType.Boutique, variantGameObject =>
                        {
                            var infoBarController = variantGameObject.GetComponent<BoutiqueInfoBarController>();

                            infoBarController.Init((IBoutiqueElement)selectedObject);
                        });
                    },
                    0);
            }));
            
            List<object> armorObjects = ArmorManager.armors.Select(v => (object)v).ToList();

            buttons.Add(("Armor", () =>
            {
                shopItemsBarController.Setup(armorObjects, 
                    boutiqueItemPrefab, 
                    (elementGameObject, elementObject) =>
                    {
                        var shopItemController = elementGameObject.GetComponent<BoutiqueItemController>();
                        
                        shopItemController.Setup((IBoutiqueElement)elementObject);
                    }, 
                    selectedObject =>
                    {
                        _infoBarController.UpdateInfo(this, InfoBarVariousType.Boutique, variantGameObject =>
                        {
                            var infoBarController = variantGameObject.GetComponent<BoutiqueInfoBarController>();

                            infoBarController.Init((IBoutiqueElement)selectedObject);
                        });
                    },
                    0);
            }));

            buttons.Add(("Ammunition", () => { }));

            buttonsBar.Init(buttons, 0);
        }
    }
}


