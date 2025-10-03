using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
    public static SoundManager Instance{get; private set;}

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) {
        if (sender is TrashCounter trashCounterRef) {
            PlaySound(audioClipRefSO.trash, trashCounterRef.transform.position);
        }
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e) {
        if (sender is BaseCounter baseCounterRef) {
            PlaySound(audioClipRefSO.objectDrop, baseCounterRef.transform.position);
        }
    }

    private void Player_OnPickedSomething(object sender, EventArgs e) {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        if (sender is CuttingCounter cuttingCounterRef) {
            PlaySound(audioClipRefSO.deliveryFail, cuttingCounterRef.transform.position);
        }
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
        PlaySound(audioClipRefSO.deliveryFail, GetComponent<Camera>().transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        PlaySound(audioClipRefSO.deliverySuccess, GetComponent<Camera>().transform.position);
    }

    private void PlaySound(AudioClip[] clipArr, Vector3 position, float volume = 1f) {
        PlaySound(clipArr[UnityEngine.Random.Range(0, clipArr.Length)], position, volume);
    } 
    
    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1f) {
        PlaySound(audioClipRefSO.footstep, position, volume);
    }
    
}
