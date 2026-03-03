using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Inputs.ITouchActions
{
    Inputs inputs;

    public static event Action<Vector2> TouchPositionEvent;
    public static event Action TapEvent;

    private void Awake() {
        inputs = new Inputs();
    }

    private void OnEnable()
    {
        inputs.Touch.SetCallbacks(this);
        inputs.Touch.Enable();
    }

    private void OnDisable() {
        inputs.Touch.Disable();
    }

    public void OnTouchPos(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            TouchPositionEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            TapEvent?.Invoke();
        }
    }
}
