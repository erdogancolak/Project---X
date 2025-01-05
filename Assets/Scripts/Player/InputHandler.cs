using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    CollectableItemController collectableItemController;
    private PlayerInput playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInput();
        playerInputs.Player.Enable();

        playerInputs.Player.Jump.performed += PlayerJump;

        playerInputs.Player.Slide.performed += PlayerSlide;

        playerInputs.Player.Attack.performed += PlayerSwordAttack;
        playerInputs.Player.Attack2.performed += PlayerSwordAttack2;

        playerInputs.Player.Ability.performed += PlayerFirstAbility;
        playerInputs.Player.Ability2.performed += PlayerSecondAbility;

        playerInputs.Player.CollectItem.performed += CollectItemInput;
    }

    

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerMovement.Instance.Jump();
        }
    }

    private void PlayerSlide(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerMovement.Instance.Slide();
        }
    }

    private void PlayerSwordAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerAttack.Instance.Attack();
            
        }
    }
    private void PlayerSwordAttack2(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerAttack.Instance.Attack2();
        }
    }
    private void PlayerFirstAbility(InputAction.CallbackContext context)
    {
        PlayerAbility.Instance.UseAbility();
    }
    private void PlayerSecondAbility(InputAction.CallbackContext context)
    {
        PlayerAbility.Instance.UseAbility2();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CollectableItems"))
        {
            collectableItemController = collision.GetComponent<CollectableItemController>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("CollectableItems"))
        {
            collectableItemController = null;
        }
    }
    private void CollectItemInput(InputAction.CallbackContext context)
    {
        if(collectableItemController != null)
        {
            collectableItemController.CollectItem();
        }
    }
}
