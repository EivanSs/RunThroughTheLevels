using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Reusable
{
    public class TextController : MonoBehaviour
    {
        [FormerlySerializedAs("Text")] public TMP_Text text;

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}
