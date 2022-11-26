using System;
using System.Collections;
using System.Collections.Generic;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class StoreInfoBarController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private TMP_Text _price;

        public void Init(CrystalsLotSettings crystalsLot)
        {
            _value.text = crystalsLot.value.ToString();
            _price.text = $"${crystalsLot.price}";
        }
    }
}

