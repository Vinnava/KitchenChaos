using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    private PlayerInputAction playerInputAction;
    
    private void Awake() {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }

    public Vector2 GetInputVectorNormalized() {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
