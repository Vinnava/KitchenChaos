using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFalied += DeliveryManager_OnRecipeFalied;
        
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
    }

    private void Player_OnPickedSomething(object sender, EventArgs e) {
        if (sender is Player playerRef) {
            PlaySound(audioClipRefSO.chop, playerRef.transform.position);
        }
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        if (sender is CuttingCounter cuttingCounterRef) {
            PlaySound(audioClipRefSO.chop, cuttingCounterRef.transform.position);
        }
    }

    private void DeliveryManager_OnRecipeFalied(object sender, EventArgs e) {
        if (Camera.main is not null) {
            DeliveryCounter deliveryCounterRef = DeliveryCounter.instance;
            PlaySound(audioClipRefSO.deliveryFail, deliveryCounterRef.transform.position);
        }
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        if (Camera.main is not null) {
            DeliveryCounter deliveryCounterRef = DeliveryCounter.instance;
            PlaySound(audioClipRefSO.deliverySuccess, deliveryCounterRef.transform.position);
        }
    }

    public void PlaySound(AudioClip[] clipArr, Vector3 position, float volume = 1f) {
        PlaySound(clipArr[UnityEngine.Random.Range(0, clipArr.Length)], position, volume);
    } 
    
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
    
}
