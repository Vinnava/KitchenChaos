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
                // player has no kitchen object 
            } else {
                // player has kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

