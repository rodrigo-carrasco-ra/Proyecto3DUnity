using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CameraManager cameraManager; 
    PlayerLocomotion playerLocomotion;
    Animator myAnimator;

    public bool isInteracting;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        inputManager=GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion=GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovements();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
        isInteracting = myAnimator.GetBool("isInteracting");
        playerLocomotion.isJumping = myAnimator.GetBool("isJumping");
        myAnimator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }


}
