using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class StoreItemController : MonoBehaviour
    {
        public Image image;
        public TMP_Text price;

        public void Setup(CrystalsLotSettings crystalsLot)
        {
            image.sprite = crystalsLot.image;
            price.text = $"${crystalsLot.price}";
        }
    }
}

