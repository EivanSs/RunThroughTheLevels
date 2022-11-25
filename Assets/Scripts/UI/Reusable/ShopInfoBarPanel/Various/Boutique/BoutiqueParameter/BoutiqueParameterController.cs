using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class BoutiqueParameterController : MonoBehaviour
    {
        public TMP_Text textValue;

        public Image Icon;

        public RectTransform valueBarContainer;

        public RectTransform valueBar;

        public List<ParameterImage> parameterImages;

        public void Setup(InfoBarParameterType parameterType, string textValue, float normalizedValue)
        {
            Icon.sprite = parameterImages.FirstOrDefault(v => v.parameterType == parameterType).image;

            this.textValue.text = textValue;

            valueBar.sizeDelta = new Vector2(valueBarContainer.rect.width * normalizedValue, valueBar.sizeDelta.y);
        }
        
        [Serializable]
        public struct ParameterImage
        {
            public Sprite image;

            public InfoBarParameterType parameterType;
        }
    }
}

