using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    
    public static Player Instance {  get; private set; }
    
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter SelectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool bIsWalking;
    private float playerRadius = 0.7f;
    private float playerHeight = 2.0f;

    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Multiple instances of Player");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnPlayerInteract += OnPlayerInteract;
    }

    private void OnPlayerInteract(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return bIsWalking;  
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        
        bool bCanMove = !Physics.CapsuleCast(
            transform.position, 
            transform.position + Vector3.up * playerHeight, playerRadius, 
            moveDir, moveDistance);

        if (!bCanMove) {
            // Cannot move towards moveDir
            
            // Attempt Only X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            bCanMove = !Physics.CapsuleCast(
                transform.position, 
                transform.position + Vector3.up * playerHeight, playerRadius, 
                moveDirX, moveDistance);

            if (bCanMove) {
                // Can move on X
                moveDir = moveDirX;
            } else {
                // Attempt Only Z
                Vector3 moveDirZ = new Vector3(moveDir.x, 0, 0).normalized;
                bCanMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight, playerRadius,
                    moveDirZ, moveDistance);
                if (bCanMove) {
                    // Can move on Z
                    moveDir = moveDirZ;
                }
            }
        }
        
        if (bCanMove) {
            transform.position += moveDir * moveDistance;
        }
        
        bIsWalking = moveDir != Vector3.zero;

        float rotationSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
    
    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
        
        float interactDistance = 2.0f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance, counterLayerMask)) {
            if (hit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }
    
    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs() {
            SelectedCounter = selectedCounter
        });
    }
    
    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject is not null;
    }
}
