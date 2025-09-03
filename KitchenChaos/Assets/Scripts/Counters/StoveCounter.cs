using System;
using UnityEngine;

public class StoveCounter : BaseCounter {

    public event EventHandler<OnStateChangedArgs> OnStateChanged;

    public class OnStateChangedArgs : EventArgs {
        public State state;
    }
    
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArr;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArr;

    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {

        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        GetKitchenObject().DestorySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        burningRecipeSO = GetBurningRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestorySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) 
        {
            // No Kitchen object
            if (player.HasKitchenObject()) {
                // player has kitchen object
                if (HasFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Has Frying Recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = state });
                    fryingTimer = 0.0f;
                };
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
                state = State.Idle;
            }
        }
    }
    
    public bool HasFryingRecipe(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOForInput(inputKitchenObjectSO);
        return fryingRecipeSO is not null;
    }

    private KitchenObjectSO GetOutForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOForInput(inputKitchenObjectSO);
        if (fryingRecipeSO is not null) 
        {
            return fryingRecipeSO.output;
        } else return null;
    }
    
    private FryingRecipeSO GetFryingRecipeSOForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArr) 
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArr) 
        {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
