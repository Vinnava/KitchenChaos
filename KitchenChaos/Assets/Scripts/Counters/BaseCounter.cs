using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    
    public static event EventHandler OnAnyObjectPlacedHere;
    
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;
    
    public virtual void Interact(Player player) {
        Debug.LogError("Interacted By BaseCounter");
    }
    
    public virtual void InteractCounter(Player player) {
        //Debug.LogError("Interacted By BaseCounter");
    }
    
    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject is not null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
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
