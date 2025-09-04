using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    
    private IHasProgress hasProgress;

    private void Start() {
        barImage.fillAmount = 0.0f;
        
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress is not null) {
            hasProgress.OnProgressChanged += HasProgress_OnProgress;
        } else Debug.LogError($"[ProgressBarUI] GameObject : {hasProgressGameObject} has no IHasProgress component!");
        
        Hide();
    }

    private void HasProgress_OnProgress(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;
        //Debug.LogWarning($"ProgressNormalized: {e.progressNormalized}");

        if (e.progressNormalized is 0.0f or 1.0f) {
            Hide();
        } else Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
