using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {
    
    [Serializable]
    public struct KithenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KithenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        
        foreach (KithenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
        
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (KithenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSOGameObject.gameObject.SetActive(true); 
            }
        }
    }
}
