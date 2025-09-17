using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeSOList recipeListSO;
    
    private List<RecipeSO>  waitingRecipeSOList;
    private float spawnRecipeTimer;
    private const float maxSpawnRecipeTimer = 4.0f;
    private int waitingRecipeMax = 4;

    private void Awake() {
        Instance = this;
        
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0) {
            spawnRecipeTimer = maxSpawnRecipeTimer;

            if (waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                //Has the same number of ingredients
                bool bPlateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    //Cycling through all the ingredients in the recipe
                    bool bIngredientMatch = false;
                    
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        //Cycling through all the ingredients in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            //Ingredient match
                            bIngredientMatch = true;
                            break;
                        }
                    }
                    if (!bIngredientMatch) {
                        // This recipe ingredient is not found on the plate
                        bPlateContentMatchesRecipe = false;
                    }
                }

                if (bPlateContentMatchesRecipe) {
                    // Player Delivered the correct recipe
                    Debug.Log("Player Delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        // No match found 
        // Player didn't deliver the correct recipe
        Debug.Log("Player didn't deliver the correct recipe");
    }
}
