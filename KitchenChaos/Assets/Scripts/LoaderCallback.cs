using UnityEngine;

public class LoaderCallback : MonoBehaviour
{

    private bool bIsFirstUpdate = true;
    
    void Update()
    {
        if (bIsFirstUpdate) {
            bIsFirstUpdate = false;
            
            Loader.LoaderCallback();
        }
    }
}
