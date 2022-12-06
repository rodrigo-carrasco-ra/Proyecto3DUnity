using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidController : MonoBehaviour
{
    Rigidbody myRigidbody = null;
    [SerializeField] HumanoidInput myInput;

    Vector3 playerMoveInput = Vector3.zero;

    [Header("Movimiento")]
    [SerializeField] float movementMuliplier = 30.0f;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerMoveInput = GetMoveInput();
        PlayerMovement();

        myRigidbody.AddRelativeForce(playerMoveInput, ForceMode.Force);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(myInput.MoveInput.x, 0.0f, myInput.MoveInput.y);

    }
    private void PlayerMovement()
    {
        playerMoveInput = (new Vector3(playerMoveInput.x * movementMuliplier * myRigidbody.mass,
                                playerMoveInput.y,
                                playerMoveInput.z * movementMuliplier * myRigidbody.mass));
    }


}
