using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Inputs.ITouchActions
{
    public static InputReader Instance { get; private set; }
    Inputs inputs;

    public event Action<Vector2> TouchPositionEvent;
    public event Action TapEvent;

    private void Awake() {
        inputs = new Inputs();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        Debug.Log("Position action triggered");

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
