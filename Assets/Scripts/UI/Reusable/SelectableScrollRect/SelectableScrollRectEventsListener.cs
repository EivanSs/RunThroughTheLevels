using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Reusable
{
    [RequireComponent(typeof(SelectableScrollRect))]
    public class SelectableScrollRectEventsListener : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private SelectableScrollRect _selectableScrollRect;

        private void Awake()
        {
            _selectableScrollRect = gameObject.GetComponent<SelectableScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _selectableScrollRect.OnBeginDrag(eventData);
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            _selectableScrollRect.OnEndDrag(eventData);
        }
    }
}
