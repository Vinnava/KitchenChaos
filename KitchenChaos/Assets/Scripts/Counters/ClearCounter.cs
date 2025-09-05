using System;
using UnityEngine;

public class ClearCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // No Kitchen object
            if (player.HasKitchenObject()) {
                // player has kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // player has no kitchen object 
            }
        } else {
            // Has Kitchen object
            if (player.HasKitchenObject()) {
                // player has kitchen object 
                if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();
                    }
                } else {
                    //player is not carrying the plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            GetKitchenObject().DestorySelf();
                        }
                    }
                }
            } else {
                // player has no kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

