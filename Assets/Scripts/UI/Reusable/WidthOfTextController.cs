using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Reusable
{
    public class WidthOfTextController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textComponent;

        [SerializeField] private float _padding;

        public float SetPadding(float value)
        {
            _padding = value;
            
            return SetWidth();
        }
        
        private void Awake()
        {
            SetWidth();
        }

        private float SetWidth()
        {
            if (_textComponent == null)
                _textComponent = gameObject.GetComponent<TMP_Text>();
            
            var symbolsCount = _textComponent.text.Length;

            var fontSize = _textComponent.fontSize;
        
            var sizeDeltaY = gameObject.GetComponent<RectTransform>().sizeDelta.y;

            var width = symbolsCount * fontSize * 0.7f + _padding;
        
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, sizeDeltaY);

            return width;
        }
    }
}

