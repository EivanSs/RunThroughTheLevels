using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UI.Controllers.Screens;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class ShopItemsBarController : MonoBehaviour
    {
        public SelectableScrollRect selectableScrollRect;

        public RectTransform itemsContainer;

        private List<(object, GameObject)> _boutiqueItems = new ();

        public void Setup(List<object> elements, GameObject prefab, Action<GameObject, object> setupElementAction, 
            Action<object> infoBarSetupAction, int firstSelectedIndex)
        {
            foreach (var boutiqueItem in _boutiqueItems)
                Destroy(boutiqueItem.Item2.gameObject);
            
            _boutiqueItems.Clear();

            foreach (var element in elements)
            {
                var item = Instantiate(prefab, itemsContainer);
                    
                setupElementAction.Invoke(item, element);

                _boutiqueItems.Add((element, item.gameObject));
            }

            selectableScrollRect.onElementSelected += index =>
            {
                var element = _boutiqueItems[index].Item1;
                
                infoBarSetupAction.Invoke(element);
            };

            float containerSpacing = itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
            
            float containerMaxWidth = selectableScrollRect.GetComponent<RectTransform>().sizeDelta.x;

            float containerPadding = containerMaxWidth / 2 - 
                                     prefab.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
            
            //itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = (int)containerPadding;
            itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().padding.right = (int)containerPadding * 2;

            float containerSuggestWidth = (elements.Count - 1) * containerSpacing + 
                                          prefab.GetComponent<RectTransform>().sizeDelta.x * elements.Count
                                   + containerPadding * 2;

            float containerWidth = Math.Max(containerMaxWidth, containerSuggestWidth);

            float sizeDeltaY = itemsContainer.sizeDelta.y;
            
            itemsContainer.sizeDelta = new Vector2(containerWidth, sizeDeltaY);

            selectableScrollRect.SetSelectedElement(firstSelectedIndex);
            
            selectableScrollRect.horizontalNormalizedPosition = 0;
        }
    }
}

