using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class BoutiaueInfoBarStateController : MonoBehaviour
{
    public TMP_Text crystalsValue;
    
    public ItemState itemState;

    public void SetCrystalText(string text)
    {
        crystalsValue.text = text;
    }
}
