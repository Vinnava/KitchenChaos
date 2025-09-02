using System;
using UnityEngine;

public class ContainerCounter : BaseCounter{
    
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player hand is empty
            Transform kitchenObjTransform = Instantiate(kitchenObjectSo.prefab);
            kitchenObjTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
