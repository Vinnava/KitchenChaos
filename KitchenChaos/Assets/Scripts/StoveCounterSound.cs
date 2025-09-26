using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    
    [SerializeField] private StoveCounter stoveCounterRef;
    
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounterRef.OnStateChanged += StoveCounterRef_OnStateChanged;
    }

    private void StoveCounterRef_OnStateChanged(object sender, StoveCounter.OnStateChangedArgs e) {
        bool playSound = e.state is StoveCounter.State.Frying or StoveCounter.State.Fried;

        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}
