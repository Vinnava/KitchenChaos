using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFalied += DeliveryManager_OnRecipeFalied;
    }

    private void DeliveryManager_OnRecipeFalied(object sender, EventArgs e) {
        throw new NotImplementedException();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        throw new NotImplementedException();
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
    
}
