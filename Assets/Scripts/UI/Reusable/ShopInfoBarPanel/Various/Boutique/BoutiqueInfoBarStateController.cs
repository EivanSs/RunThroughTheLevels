using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Reusable
{
    public class BoutiqueInfoBarStateController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _crystalsValue;
    
        [SerializeField] private ItemState _itemState;
    
        public ItemState itemState => _itemState;

        public void SetCrystalText(string text)
        {
            _crystalsValue.text = text;
        }
    }
}

