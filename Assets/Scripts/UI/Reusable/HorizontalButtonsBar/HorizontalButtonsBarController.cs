using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class HorizontalButtonsBarController : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonPrefab;

        [SerializeField] private GameObject _scrollContainer;

        [SerializeField] private ScrollRect _containerScrollRect;
    
        private List<DualStateController> _buttonStates = new();

        public void Init(List<(string, Action)> buttons, int defaultButtonIndex)
        {
            float mainButtonsWidth = 0;
            
            foreach (var buttonSetting in buttons)
            {
                var buttonObject = Instantiate(_buttonPrefab, _scrollContainer.GetComponent<RectTransform>());
            
                buttonObject.GetComponent<TextController>().SetText(buttonSetting.Item1);
            
                mainButtonsWidth += buttonObject.GetComponent<WidthOfTextController>().SetPadding(150);

                var buttonState = buttonObject.GetComponent<DualStateController>();

                buttonObject.GetComponent<Button>().onClick.AddListener(ApplyButton);

                if (buttons.IndexOf(buttonSetting) == defaultButtonIndex)
                    ApplyButton();

                void ApplyButton()
                {
                    buttonSetting.Item2.Invoke();
                
                    foreach (var buttonState in _buttonStates)
                        buttonState.Deactivate();

                    buttonState.Activate();
                }
            
                _buttonStates.Add(buttonState);
            }

            var sizeDeltaY = _scrollContainer.GetComponent<RectTransform>().sizeDelta.y;

            var spacing = _scrollContainer.GetComponent<HorizontalLayoutGroup>().spacing;

            float width = Math.Max((buttons.Count - 1) * spacing + mainButtonsWidth, 
                _containerScrollRect.GetComponent<RectTransform>().sizeDelta.x);

            _scrollContainer.GetComponent<RectTransform>().sizeDelta = 
                new Vector2(width, sizeDeltaY);

            _containerScrollRect.horizontalNormalizedPosition = 0;
        }
    }
}

