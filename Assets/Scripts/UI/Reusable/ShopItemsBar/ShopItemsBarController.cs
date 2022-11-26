 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UI.Controllers.Screens;
using UnityEngine;
using UnityEngine.EventSystems;
 using UnityEngine.Serialization;
 using UnityEngine.UI;

namespace UI.Reusable
{
    public class ShopItemsBarController : MonoBehaviour
    {
        [SerializeField] private ShopBarMode _shopBarMode;
        
        [SerializeField] private SelectableScrollRect _selectableScrollRect;

        [SerializeField] private RectTransform _itemsContainer;

        private List<(object, GameObject)> _storeItems = new ();

        public void Setup(List<object> elements, GameObject prefab, Action<GameObject, object> setupElementAction, 
            Action<object> infoBarSetupAction, int firstSelectedIndex)
        {
            foreach (var boutiqueItem in _storeItems)
                Destroy(boutiqueItem.Item2.gameObject);
            
            _storeItems.Clear();

            foreach (var element in elements)
            {
                var item = Instantiate(prefab, _itemsContainer);
                    
                setupElementAction.Invoke(item, element);

                _storeItems.Add((element, item.gameObject));
            }

            _selectableScrollRect.onElementSelected += index =>
            {
                var element = _storeItems[index].Item1;
                
                infoBarSetupAction.Invoke(element);
            };

            float containerSpacing = _itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
            
            float containerMaxWidth = _selectableScrollRect.GetComponent<RectTransform>().sizeDelta.x;

            float containerPadding = containerMaxWidth / 2 - 
                                     prefab.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;

            switch (_shopBarMode)
            {
                case ShopBarMode.ItemsLeft:
                    _itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().padding.right = (int)containerPadding * 2;
                    
                    break;
                
                case ShopBarMode.ItemsCenter:
                    _itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = (int)containerPadding;
                    _itemsContainer.gameObject.GetComponent<HorizontalLayoutGroup>().padding.right = (int)containerPadding;
                    
                    break;
            }

            float containerSuggestWidth = (elements.Count - 1) * containerSpacing + 
                                          prefab.GetComponent<RectTransform>().sizeDelta.x * elements.Count
                                          + containerPadding * 2;

            float containerWidth = Math.Max(containerMaxWidth, containerSuggestWidth);

            float sizeDeltaY = _itemsContainer.sizeDelta.y;
            
            _itemsContainer.sizeDelta = new Vector2(containerWidth, sizeDeltaY);

            _selectableScrollRect.SetSelectedElement(firstSelectedIndex);
            
            _selectableScrollRect.horizontalNormalizedPosition = 0;
        }
        
        public enum ShopBarMode
        {
            ItemsLeft,
            ItemsCenter
        }
    }
}

