using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    
    public event EventHandler OnPlateComplete;

    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
    private IKitchenObjectParent kitchenObjectParent;

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
    
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSo;
    }
    
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent is not null) 
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        
        if (kitchenObjectParent.HasKitchenObject()) 
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
        }
        
        kitchenObjectParent.SetKitchenObject(this);
        transform.SetParent(kitchenObjectParent.GetKitchenObjectFollowTransform());
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = (PlateKitchenObject)this;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }

    public void DestorySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
