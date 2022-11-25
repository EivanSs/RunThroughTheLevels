using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UI.Controllers.Screens;
using UnityEngine;

namespace UI.Reusable
{
    public class InfoBarController : MonoBehaviour
    {
        public RectTransform movableContent;

        public List<InfoBarVariant> infoBarVariants;

        private BaseScreen _barOwner;

        private GameObject _variantObject;
        
        public void Init(BaseScreen barOwner)
        {
            _barOwner = barOwner;
        }

        public void UpdateInfo(BaseScreen caller, InfoBarVariousType barType, Action<GameObject> setupVariantAction)
        {
            if (caller != _barOwner)
                return;
            
            if (_variantObject != null)
                Destroy(_variantObject);

            _variantObject = Instantiate(infoBarVariants
                .First(v => v.barType == barType).gameObject, movableContent);
            
            setupVariantAction.Invoke(_variantObject);
        }

        public void Show()
        {
            movableContent.anchoredPosition = new Vector2(0, -121);

            movableContent.DOLocalMoveY(0, 0.5f);
        }

        public void HideAndDestroy()
        {
            movableContent.DOLocalMoveY(-121, 0.5f).onComplete += () =>
            {
                Destroy(gameObject);
            };
        }
    }
    
    [Serializable]
    
    public struct InfoBarVariant
    {
        public InfoBarVariousType barType;

        public GameObject gameObject;
    }
        
    public enum InfoBarVariousType
    {
        Boutique,
        Store,
        Upgrade
    }
}

