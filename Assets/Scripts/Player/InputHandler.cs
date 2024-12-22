using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInput();
        playerInputs.Player.Enable();

        playerInputs.Player.Jump.performed += PlayerJump;
        playerInputs.Player.SwordAttack.performed += PlayerSwordAttack;
        playerInputs.Player.SwordAttack2.performed += PlayerSwordAttack2;
    }

    private void PlayerSwordAttack2(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerAttack.Instance.Attack2();
        }
    }

    private void PlayerSwordAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerAttack.Instance.Attack();
        }
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerMovement.Instance.Jump();
        }
    }


}
