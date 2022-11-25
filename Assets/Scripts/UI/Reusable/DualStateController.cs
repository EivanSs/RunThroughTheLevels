using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Reusable
{
    public class DualStateController : MonoBehaviour
    {
        public GameObject activeState;
        public GameObject disabledState;

        public void Active()
        {
            activeState.SetActive(true);
            disabledState.SetActive(false);
        }

        public void Disable()
        {
            activeState.SetActive(false);
            disabledState.SetActive(true);
        }
    }
}
