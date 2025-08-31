using System;
using UnityEditor.Experimental;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject visualGameObject;
    
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += InstanceOnOnSelectedCounterChanged;
    }

    private void InstanceOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.SelectedCounter == clearCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        visualGameObject.SetActive(true);
    }

    private void Hide() {
        visualGameObject.SetActive(false);
    }
}
