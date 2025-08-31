using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public EventHandler OnPlayerInteract;
    
    private PlayerInputAction playerInputAction;
    
    private void Awake() {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        
        playerInputAction.Player.Interact.performed += InteractOnPerformed;
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj) {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
