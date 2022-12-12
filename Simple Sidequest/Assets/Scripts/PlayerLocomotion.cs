using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    InputManager inputManager;
    AnimatorManager animatorManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody myRigidBody;

    [Header("Heroina cayendo")]
    public float inAirTimer;
    public float fallingVelocity;
    public float leapingVelocity;
    public LayerMask groundLayer;
    public float raycastHeightOffset = 0.5f; 

    [Header("Tipos de movimiento")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Velocidad de movimiento")]
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float rotationSpeed;

    [Header("Gestor de salto")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        myRigidBody = GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        HandleFallingnLanding();
        if (playerManager.isInteracting) return; //si hay interaccion, o sea se hace otra cosa que no sea movimiento se regresa lo siguiente:
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        if (isJumping) return;
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0; //para no irse al cielo

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkSpeed;
            }
        }

        Vector3 movementVelocity = moveDirection;
        myRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping) return;
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation,rotationSpeed*Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingnLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y = raycastOrigin.y + raycastHeightOffset;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }
            inAirTimer = inAirTimer + Time.deltaTime;
            myRigidBody.AddForce(transform.forward * leapingVelocity);
            myRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer); //-Vector3.up es igual a "down"
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            animatorManager.myAnimator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity; //conserva la velocidad para el salto
            myRigidBody.velocity = playerVelocity;
        }
    }
}
