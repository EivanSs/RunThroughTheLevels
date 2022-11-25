using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reusable
{
    public class BoutiqueItemController : MonoBehaviour
    {
        public TMP_Text title;

        public Button selectButton;

        public GameObject blockedBuyLevel_Moneyss;
        public GameObject blockedBuyLevel_Crystalls;
        
        public List<BoutiqueItemStateController> itemStateControllers;

        public void Setup(IBoutiqueElement boutiqueElement)
        {
            foreach (var itemStateController in itemStateControllers)
                itemStateController.Setup(boutiqueElement);

            title.text = boutiqueElement.GetTitle();

            foreach (var stateController in itemStateControllers)
            {
                if (boutiqueElement.GetItemState(0) == ItemState.BlockedBuyLevel)
                {
                    if (boutiqueElement.PurchaseSettings().moneys == 0)
                    {
                        blockedBuyLevel_Crystalls.SetActive(true);
                        blockedBuyLevel_Moneyss.SetActive(false);
                    }
                    else
                    {
                        blockedBuyLevel_Moneyss.SetActive(true);
                        blockedBuyLevel_Crystalls.SetActive(false);
                    }
                }
                else
                    stateController.gameObject.SetActive(stateController.itemState == boutiqueElement.GetItemState(0));
            }
        }
    }
}

