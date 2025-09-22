using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Transform iconContainer;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) {
            Transform recipeTransform = Instantiate(iconTemplate, iconContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
