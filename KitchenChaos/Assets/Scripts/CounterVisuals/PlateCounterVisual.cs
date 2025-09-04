using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour {
    
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    
    private List<GameObject> platesVisualGameObjectList;

    private void Awake() {
        platesVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, EventArgs e) {
        GameObject plateGameObject = platesVisualGameObjectList[^1];
        platesVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0.0f, plateOffsetY * platesVisualGameObjectList.Count, 0.0f);
        
        platesVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
