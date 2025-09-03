using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start() {
        barImage.fillAmount = 0.0f;

        if (cuttingCounter is not null) {
            cuttingCounter.OnProgressChanged += CuttingCounterOnOnProgressChanged;
        } else Debug.LogError("[ProgressBarUI] Cutting counter is null.");
        
        Hide();
    }

    private void CuttingCounterOnOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized is not 1.0f or 0.0f) {
            Show();
        } else Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
