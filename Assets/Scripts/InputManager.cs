using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public event Action<Vector2> OnContinuousEvent;
    public event Action<bool> OnTapEvent;
    private bool _isTouching = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isTouching = true;
            OnTapEvent?.Invoke(true);
            Debug.Log("Touch started");
        }
        else if (context.canceled)
        {
            OnTapEvent?.Invoke(false);
            _isTouching = false;
            Debug.Log("Touch ended");
        }
    }

    public void OnContinuous(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Debug.Log($"Continuous input value: {value}");
        OnContinuousEvent?.Invoke(value);
    }
}

