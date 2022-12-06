using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanoidInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed = false;
    public Vector2 LookInput { get; private set; } = Vector2.zero;

    InputActions myInput =null;

    private void OnEnable()
    {
        myInput = new InputActions();
        myInput.Humanoid.Enable();

        myInput.Humanoid.Move.performed += SetMove;
        myInput.Humanoid.Move.canceled += SetMove;

        myInput.Humanoid.Look.performed += SetLook;
        myInput.Humanoid.Look.canceled += SetLook;

    }

    private void OnDisable()
    {
        myInput.Humanoid.Move.performed -= SetMove;
        myInput.Humanoid.Move.canceled -= SetMove;

        myInput.Humanoid.Look.performed -= SetLook;
        myInput.Humanoid.Look.canceled -= SetLook;

        myInput.Humanoid.Disable();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput=ctx.ReadValue<Vector2>();
        MoveIsPressed =! (MoveInput== Vector2.zero);
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }
}
