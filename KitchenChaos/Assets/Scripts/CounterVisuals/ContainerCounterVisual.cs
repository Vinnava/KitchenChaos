using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {
    
    private static String OPEN_CLOSE = "OpenClose";
    
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrabbedObject += ContainerCounterOnPlayerGrabbedObject;
    }

    private void ContainerCounterOnPlayerGrabbedObject(object sender, EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
