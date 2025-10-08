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
            SceneManager.LoadScene(1);
        }
        
        void QuitClick() {
            Application.Quit();
        }
    }
    
}
