using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField]  private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(PlayClick);
        quitButton.onClick.AddListener(QuitClick);
        return;

        void PlayClick() {
            Loader.Load(Loader.Scene.GameScene);
        }
        
        void QuitClick() {
            Application.Quit();
        }
    }
    
}
