using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArr;
    
    private int cuttingProgress;
    
    public override void Interact(Player player) 
    {
        if (!HasKitchenObject()) 
        {
            // No Kitchen object
            if (player.HasKitchenObject()) 
            {
                // player has kitchen object
                if (HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO())) 
                {
                    // Has Cutting Recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            } else 
            {
                // player has no kitchen object 
            }
        } else 
        {
            // Has Kitchen object
            if (player.HasKitchenObject()) {
                // player has no kitchen object 
            } else 
            {
                // player has kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                    progressNormalized = 0
                });
            }
        }
    }

    public override void InteractCounter(Player player) 
    {
        if (HasKitchenObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO())) 
        {
            // Has kitchen Object & It has recipe
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            Debug.Log("Cutting");
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            if (cuttingProgress == cuttingRecipeSO.cuttingProgressMax) 
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestorySelf();
                Debug.Log($"Sliced: {outputKitchenObjectSO.objectName}");

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    public bool HasCuttingRecipe(KitchenObjectSO inputKitchenObjectSO) 
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(inputKitchenObjectSO);
        return cuttingRecipeSO is not null;
    }

    private KitchenObjectSO GetOutForInput(KitchenObjectSO inputKitchenObjectSO) 
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(inputKitchenObjectSO);
        if (cuttingRecipeSO is not null) 
        {
            return cuttingRecipeSO.output;
        } else return null;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO inputKitchenObjectSO) 
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArr) 
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) 
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
