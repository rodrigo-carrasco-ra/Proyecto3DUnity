using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Camara")]
    public Camera playerCamera;
    public float rotationSensibility= 50f;

    [Header("Movimiento Jugador")]
    public float walkSpeed=5f;
    public float runSpeed = 10f;

    [Header("Salto")]
    public float jumpHeight = 3f;
    public float gravity = -9.8f;

    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;
    [SerializeField] AudioSource walkSound;

    private void Start()
    {
        walkSound = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Look();
    }

    public void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            walkSound.Play();

            if (Input.GetButton("Run"))

            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            } 
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }
            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
                walkSound.Stop();

            }
        }
        moveInput.y += gravity * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);

    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0, 0); // solo rotara en x 
    }
}
