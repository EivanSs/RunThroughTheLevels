using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UI.Reusable
{
    [RequireComponent(typeof(SelectableScrollRectEventsListener))]
    public class SelectableScrollRect : ScrollRect
    {
        private RectTransform _thisRectTransform;
        
        private int _selectedElementIndex;

        private int _itemsCount;

        public Action<int> onElementSelected;
        
        private void Awake()
        {
            _thisRectTransform = gameObject.GetComponent<RectTransform>();
        }

        public int GetSelectedElement()
        {
            return _selectedElementIndex;
        }

        public void SetSelectedElement(int index)
        {
            UpdateSelectedElement(index);
        }

        private void Update()
        {
            int newElementIndex = 0;
            
            float itemPortion = 1f / (content.childCount - 1);
    
            for (int i = 0; i < content.childCount; i++)
            {
                if (Math.Abs(GetItemPosition(i)) < itemPortion / 2)
                    newElementIndex = i;
            }

            if (newElementIndex != _selectedElementIndex)
                UpdateSelectedElement(newElementIndex);

            float movingSpeed = content.childCount / 0.16f;

            velocity -= new Vector2(GetItemPosition(_selectedElementIndex) * movingSpeed, 0);
        }
    
        private void UpdateSelectedElement(int newElementIndex)
        {
            _selectedElementIndex = newElementIndex;
            
            onElementSelected?.Invoke(newElementIndex);
        }
    
        private float GetItemPosition(int index)
        {
            float itemPortion = 1f / (content.childCount - 1);
    
            float itemLocalPosition = index * itemPortion;

            float scrollProgress = content.anchoredPosition.x *
                                    (content.rect.width / (content.rect.width - _thisRectTransform.rect.width))
                                    / content.rect.width * -1;

            return itemLocalPosition - scrollProgress;
        }
    }
}

