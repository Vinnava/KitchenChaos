using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    
    [SerializeField] private RecipeSOList recipeListSO;
    
    private List<RecipeSO>  waitingRecipeSOList;
    private float spawnRecipeTimer;
    private const float maxSpawnRecipeTimer = 4.0f;

    private void Awake() {
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0) {
            spawnRecipeTimer = maxSpawnRecipeTimer;
            
            RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
        }
    }
}
