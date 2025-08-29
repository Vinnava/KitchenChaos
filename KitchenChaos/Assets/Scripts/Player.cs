using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    
    private bool bIsWalking;
    private float playerRadius = 0.7f;
    private float playerHeight = 2.0f;
    
    // Update is called once per frame
    private void Update() {
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

    public bool IsWalking() {
        return bIsWalking;  
    }
}
