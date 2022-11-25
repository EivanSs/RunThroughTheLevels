using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/StoreSettings")]
    public class StoreSettings : ScriptableObject
    {
        public List<CrystalsLotSettings> crystalsLots;
    }

    [Serializable]
    public struct CrystalsLotSettings
    {
        public int value;
        public Sprite image;
        public float price;
    }
}
