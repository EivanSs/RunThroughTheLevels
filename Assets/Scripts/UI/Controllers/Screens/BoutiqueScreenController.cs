using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UI.Reusable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Controllers.Screens
{
    public class BoutiqueScreenController : BaseScreen
    {
        [SerializeField] private GameObject _boutiqueItemPrefab;
        
        [SerializeField] private Button _buttonBack;
        
        [SerializeField] private HorizontalButtonsBarController _buttonsBar;

        [SerializeField] private ShopItemsBarController _shopItemsBarController;

        private SelectableScrollRect _selectableScrollRect => _shopItemsBarController.GetComponent<SelectableScrollRect>();

        public override bool enableInfoBar
        {
            get
            {
                return true;
            }
        }
        
        public void Start()
        {
            _buttonBack.onClick.AddListener(() =>
            {
                ScreensManager.OpenScreen(ScreenName.MainScreen, ScreenTransitionType.MoveRight);
                
                HeaderController.ShowHeader();
            });
            
            List<(string, Action)> buttons = new List<(string, Action)>();

            List<object> weaponObjects = WeaponsManager.weapons.Select(v => (object)v).ToList();

            buttons.Add(("Weapons", () =>
            {
                _shopItemsBarController.Setup(weaponObjects, 
                    _boutiqueItemPrefab, 
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
                _shopItemsBarController.Setup(armorObjects, 
                    _boutiqueItemPrefab, 
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

            _buttonsBar.Init(buttons, 0);
        }
    }
}


