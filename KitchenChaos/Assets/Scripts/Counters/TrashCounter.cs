using System;

public class TrashCounter : BaseCounter {
    
    public static event EventHandler OnAnyObjectTrashed; 
    
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestorySelf();
        }
    }
}
