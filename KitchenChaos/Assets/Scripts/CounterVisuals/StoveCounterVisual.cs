using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveGameObject;
    [SerializeField] private GameObject particleGameObject;

    private void Start() {
        if (stoveCounter) {
            stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
        }
    }

    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedArgs e) {
        bool bShowVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveGameObject.SetActive(bShowVisual);
        particleGameObject.SetActive(bShowVisual);
    }
}
