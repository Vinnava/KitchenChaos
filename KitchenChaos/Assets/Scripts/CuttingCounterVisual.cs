using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    
    private static String CUT = "Cut";
    
    [SerializeField] private CuttingCounter cuttingCounter;
    
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnCut += OnCut;
    }

    private void OnCut(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
