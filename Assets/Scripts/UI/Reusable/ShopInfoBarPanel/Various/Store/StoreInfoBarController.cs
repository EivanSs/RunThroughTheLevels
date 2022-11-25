using System;
using System.Collections;
using System.Collections.Generic;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class StoreInfoBarController : MonoBehaviour
    {
        public TMP_Text value;
        public TMP_Text price;

        public void Init(CrystalsLotSettings crystalsLot)
        {
            value.text = crystalsLot.value.ToString();
            price.text = $"${crystalsLot.price}";
        }
    }
}

