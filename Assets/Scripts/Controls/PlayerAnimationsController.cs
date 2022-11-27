using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    public void Stay()
    {
        _playerAnimator.SetBool("Run", false);

        _playerAnimator.speed = 1;
    }

    public void Run(float runSpeed)
    {
        _playerAnimator.SetBool("Run", true);

        _playerAnimator.speed = runSpeed;
    }
}
