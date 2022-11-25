using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Reusable
{
    public class BoutiqueItemStateController : MonoBehaviour
    {
        public TMP_Text description;
        public TMP_Text moneys;
        public TMP_Text crystals;

        public ItemState itemState;
        
        public void Setup(IBoutiqueElement boutiqueElement)
        {
            if (description != null)
                description.text = boutiqueElement.GetDescription();
            
            if (moneys != null)
                moneys.text = boutiqueElement.PurchaseSettings().moneys.ToString();
            
            if (crystals != null)
                crystals.text = boutiqueElement.PurchaseSettings().crystals.ToString();
        }
    }
}

