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
        public GameObject buttonPrefab;

        public GameObject scrollContainer;

        public ScrollRect containerScrollRect;
    
        private List<DualStateController> _buttonStates = new();

        public void Init(List<(string, Action)> buttons, int defaultButtonIndex)
        {
            float mainButtonsWidth = 0;
            
            foreach (var buttonSetting in buttons)
            {
                var buttonObject = Instantiate(buttonPrefab, scrollContainer.GetComponent<RectTransform>());
            
                buttonObject.GetComponent<TextController>().SetText(buttonSetting.Item1);
            
                mainButtonsWidth += buttonObject.GetComponent<WidthOfTextController>().SetPadding(150);

                var buttonState = buttonObject.GetComponent<DualStateController>();

                buttonObject.GetComponent<Button>().onClick.AddListener(OnButtonClick);

                if (buttons.IndexOf(buttonSetting) == defaultButtonIndex)
                    OnButtonClick();

                void OnButtonClick()
                {
                    buttonSetting.Item2.Invoke();
                
                    foreach (var buttonState in _buttonStates)
                        buttonState.Disable();

                    buttonState.Active();
                }
            
                _buttonStates.Add(buttonState);
            }

            var sizeDeltaY = scrollContainer.GetComponent<RectTransform>().sizeDelta.y;

            var spacing = scrollContainer.GetComponent<HorizontalLayoutGroup>().spacing;

            float width = Math.Max((buttons.Count - 1) * spacing + mainButtonsWidth, 
                containerScrollRect.GetComponent<RectTransform>().sizeDelta.x);

            scrollContainer.GetComponent<RectTransform>().sizeDelta = 
                new Vector2(width, sizeDeltaY);

            containerScrollRect.horizontalNormalizedPosition = 0;
        }
    }
}

