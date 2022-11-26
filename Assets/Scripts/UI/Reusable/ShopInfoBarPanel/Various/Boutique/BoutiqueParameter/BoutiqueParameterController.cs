using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class BoutiqueParameterController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textValue;

        [SerializeField] private Image _icon;

        [SerializeField] private RectTransform _valueBarContainer;

        [SerializeField] private RectTransform _valueBar;

        [SerializeField] private List<ParameterImage> _parameterImages;

        public void Setup(InfoBarParameterType parameterType, string textValue, float normalizedValue)
        {
            _icon.sprite = _parameterImages.FirstOrDefault(v => v.parameterType == parameterType).image;

            _textValue.text = textValue;

            _valueBar.sizeDelta = new Vector2(_valueBarContainer.rect.width * normalizedValue, _valueBar.sizeDelta.y);
        }
        
        [Serializable]
        public struct ParameterImage
        {
            public Sprite image;

            public InfoBarParameterType parameterType;
        }
    }
}

