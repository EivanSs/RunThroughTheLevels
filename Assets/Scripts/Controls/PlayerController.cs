using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controls
{
    public class PlayerController : MonoBehaviour
    {
        private const float maxVelocity = 11;
        
        [SerializeField] private float _directionMultiplier = 1;

        [SerializeField] private float _directionClamp = 20;

        [SerializeField] private float _velocityCaf = 1;

        [SerializeField] private float _directionDel = 1.3f;
        
        private Joystick _joystick;

        private Rigidbody _rigidbody;

        private Vector2 _direction;

        private PlayerAnimationsController _animationsController;
        
        private void Awake()
        {
            _joystick = FindObjectOfType<Joystick>();

            _rigidbody = GetComponent<Rigidbody>();

            _animationsController = GetComponent<PlayerAnimationsController>();
        }

        private void FixedUpdate()
        {
             _direction += _joystick.Direction * _directionMultiplier;

             _direction = new Vector2(Math.Clamp(_direction.x, -_directionClamp, _directionClamp), Math.Clamp(_direction.y, -_directionClamp, _directionClamp));

            _rigidbody.velocity = new Vector3(_direction.x * _velocityCaf, 0, _direction.y * _velocityCaf);
            
            var playerRotation = Quaternion.Euler(0, (float)Math.Atan2(_rigidbody.velocity.x, _rigidbody.velocity.z) * Mathf.Rad2Deg, 0);
            
            _rigidbody.MoveRotation(playerRotation);
            
            _direction /= _directionDel;
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude < 0.2f)
                _animationsController.Stay();
            else
                _animationsController.Run(_rigidbody.velocity.magnitude / maxVelocity);
        }
    }
}

