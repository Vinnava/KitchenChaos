using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {
    
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;
    
    public override void Interact(Player player) {
        if (kitchenObject is null) {
            Transform kitchenObjTransform = Instantiate(kitchenObjectSo.prefab, counterTopPoint);
            kitchenObjTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            // Give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
    
    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
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
