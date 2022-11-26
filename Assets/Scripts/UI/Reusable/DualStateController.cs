using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Reusable
{
    public class DualStateController : MonoBehaviour
    {
        [SerializeField] private GameObject _activeState;
        [SerializeField] private GameObject _disabledState;

        public void Activate()
        {
            _activeState.SetActive(true);
            _disabledState.SetActive(false);
        }

        public void Deactivate()
        {
            _activeState.SetActive(false);
            _disabledState.SetActive(true);
        }
    }
}
