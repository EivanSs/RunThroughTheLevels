using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Utils;

namespace Controls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _chaseObject;

        [SerializeField] private float _cameraAngle = 0;

        [SerializeField] private float _cameraDistance = 10;

        private void FixedUpdate()
        {
            float angle = 90 + _cameraAngle;
            
            var requiredPosition = _chaseObject.position + 
                                   new Vector3(0, (float)Math.Sin(angle * MathUtils.DegToRad), 
                                       (float)Math.Cos(angle * MathUtils.DegToRad)) * _cameraDistance;
            
            GetComponent<Transform>().rotation = Quaternion.Euler(90 -_cameraAngle, 0, 0);

            GetComponent<Transform>().position = requiredPosition;
        }
    }
}

