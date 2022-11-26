using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class BoutiqueInfoBarController : MonoBehaviour
    {
        [SerializeField] private BoutiqueParameterController _parameterPrefab;
    
        [SerializeField] private TMP_Text _title;

        [SerializeField] private RectTransform _parametersBar;

        [SerializeField] private List<BoutiqueInfoBarStateController> _barStates;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _buyForCrystalsButton;
        [SerializeField] private Button _buyForCrystalsButton2;
        [SerializeField] private Button _use;
        [SerializeField] private Button _upgrade;

        private IBoutiqueElement _boutiqueElement;

        private bool _inited;

        private void Awake()
        {
            _buyButton.onClick.AddListener(Buy);
            
            _buyForCrystalsButton.onClick.AddListener(BuyForCrystals);
            
            _buyForCrystalsButton2.onClick.AddListener(BuyForCrystals);
            
            _use.onClick.AddListener(Use);
            
            _upgrade.onClick.AddListener(Upgrade);
        }

        public void Init(IBoutiqueElement boutiqueElement)
        {
            _title.text = $"{boutiqueElement.GetTitle()}:";

            var elementInfoBarSettings = boutiqueElement.InfoBarSettings();

            if (elementInfoBarSettings.withParameters)
            {
                foreach (var parameter in elementInfoBarSettings.parameters)
                {
                    var parameterInstance = Instantiate(_parameterPrefab, _parametersBar);
                    
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

