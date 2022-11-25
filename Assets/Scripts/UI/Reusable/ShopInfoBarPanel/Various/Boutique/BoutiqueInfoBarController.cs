using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class BoutiqueInfoBarController : MonoBehaviour
    {
        public BoutiqueParameterController parameterPrefab;
    
        public TMP_Text title;

        public RectTransform parametersBar;

        public List<BoutiaueInfoBarStateController> _barStates;

        public Button buyButton;
        public Button buyForCrystalsButton;
        public Button buyForCrystalsButton2;
        public Button use;
        public Button upgrade;

        private IBoutiqueElement _boutiqueElement;

        private bool _inited;

        private void Awake()
        {
            buyButton.onClick.AddListener(Buy);
            
            buyForCrystalsButton.onClick.AddListener(BuyForCrystals);
            
            buyForCrystalsButton2.onClick.AddListener(BuyForCrystals);
            
            use.onClick.AddListener(Use);
            
            upgrade.onClick.AddListener(Upgrade);
        }

        public void Init(IBoutiqueElement boutiqueElement)
        {
            title.text = $"{boutiqueElement.GetTitle()}:";

            var elementInfoBarSettings = boutiqueElement.ShopInfoBarSettings();

            if (elementInfoBarSettings.withParameters)
            {
                foreach (var parameter in elementInfoBarSettings.parameters)
                {
                    var parameterInstance = Instantiate(parameterPrefab, parametersBar);
                    
                    parameterInstance.Setup(parameter.parameterType, parameter.textValue, parameter.normalizedValue);
                }
            }

            foreach (var barState in _barStates)
            {
                switch (barState.itemState)
                {
                    case ItemState.Buy:
                        barState.SetCrystalText($"or {boutiqueElement.PurchaseSettings().crystals}");
                        
                        break;
                    case ItemState.BuyForCrystals:
                        barState.SetCrystalText(boutiqueElement.PurchaseSettings().crystals.ToString());
                        
                        break;
                }
                
                barState.gameObject.SetActive(barState.itemState == boutiqueElement.GetItemState(0));
            }
            
            _boutiqueElement = boutiqueElement;

            _inited = true;
        }

        private void Buy()
        {
            if (!_inited)
                return;
            
            if (_boutiqueElement.GetItemState(0) != ItemState.Buy)
                return;
            
        }

        private void BuyForCrystals()
        {
            if (!_inited)
                return;
            
            if (!(_boutiqueElement.GetItemState(0) == ItemState.Buy || _boutiqueElement.GetItemState(0) != ItemState.BuyForCrystals))
                return;
            
        }

        private void Use()
        {
            if (!_inited)
                return;
            
            if (_boutiqueElement.GetItemState(0) != ItemState.Purchased)
                return;
            
        }

        private void Upgrade()
        {
            if (_boutiqueElement.GetItemState(0) != ItemState.Purchased)
                return;
            
            ScreensManager.OpenScreen(ScreenName.UpgradeScreen, ScreenTransitionType.MoveUp);
        }
    }
}

