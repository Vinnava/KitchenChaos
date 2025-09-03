using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public event EventHandler OnPlayerInteract;
    public event EventHandler OnPlayerInteractCounter;
    
    private PlayerInputAction playerInputAction;
    
    private void Awake() {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        
        playerInputAction.Player.Interact.performed += InteractOnPerformed;
        playerInputAction.Player.InteractCounter.performed += InteractCounterOnPerformed;
    }
    
    private void InteractOnPerformed(InputAction.CallbackContext obj) {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
    }
    
    private void InteractCounterOnPerformed(InputAction.CallbackContext obj) {
        if (OnPlayerInteractCounter != null) {
            OnPlayerInteractCounter.Invoke(this, EventArgs.Empty);
        } else {
            Debug.LogWarning("[GameInput] OnPlayerInteractCounter has no subscribers!");
        }
    }

    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
