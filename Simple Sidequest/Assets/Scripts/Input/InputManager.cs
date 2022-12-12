using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion myplayerLocomotion;
    AnimatorManager myAnimatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool b_Input;
    public bool space_Input;

    private void Awake()
    {
        myAnimatorManager = GetComponent<AnimatorManager>();
        myplayerLocomotion = GetComponent<PlayerLocomotion>();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovements.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovements.Camera.performed += i => cameraInput = i.ReadValue<Vector2>(); //mandando valores del mouse a camera input

            playerControls.PlayerActions.Strafe.performed += i => b_Input = true;
            playerControls.PlayerActions.Strafe.canceled += i => b_Input = false;
            playerControls.PlayerActions.Jump.performed += i => space_Input = true;


        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintIntput();
        HandleJumpingInput();
        //handleactioninput
    }



    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y; 
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));  //valor absoluto 
        myAnimatorManager.UpdateAnimatorValues(0, moveAmount, myplayerLocomotion.isSprinting); ;
    }

    private void HandleSprintIntput()
    {
        if (b_Input && moveAmount > 0.5f)
        {
            myplayerLocomotion.isSprinting = true;
        }
        else
        {
            myplayerLocomotion.isSprinting = false;

        }
    }

    private void HandleJumpingInput()
    {
        if (space_Input)
        {
            space_Input = false;
            myplayerLocomotion.HandleJump();
        }
    }
}
