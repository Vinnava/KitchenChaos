using UnityEngine;

public class DeliveryCounter : BaseCounter {
    
    public static DeliveryCounter instance {get; private set;}

    private void Awake() {
        instance = this;
    }
    
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestorySelf();
            }
        }
    }
}
