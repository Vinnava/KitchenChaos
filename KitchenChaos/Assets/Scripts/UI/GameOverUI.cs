using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI recipeCountText;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            recipeCountText.text = Mathf.Ceil(DeliveryManager.Instance.GetSuccessfulRecipeCount()).ToString();
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
