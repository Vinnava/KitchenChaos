using System;
using UnityEngine;

public class PlateCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnTimer;
    private float spawnTimerMax = 4.0f;
    private int plateCount;
    private int plateCountMax = 4;

    private void Update() {
        spawnTimer += Time.deltaTime;
        //Debug.Log("Plate spawnTimer: " + spawnTimer);
        if (spawnTimer  > spawnTimerMax) {
            spawnTimer = 0;
            
            if (plateCount < plateCountMax) {
                plateCount++;
                
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // player Empty Handed
            if (plateCount > 0) {
                plateCount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
