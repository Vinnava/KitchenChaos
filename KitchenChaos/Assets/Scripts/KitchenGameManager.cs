using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
    
    public static KitchenGameManager Instance{ get; private set;}
    
    public event EventHandler OnStateChanged;
    
    private enum State {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    
    private float waitingToStartTimer = 1.0f;
    private float countdownToStartTimer = 3.0f;
    private float gamePlayingTimer;
    private float GamePlayingTimerMax = 10.0f;
    
    private bool bIsGamePause = false;
    
    private void Awake() {
        state = State.WaitingToStart;
        
        Instance = this;
    }

    private void OnEnable() {
        GameInput.Instance.OnPlayerPause += GameInput_OnPlayerPause;
    }

    private void GameInput_OnPlayerPause(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0.0f) {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0.0f) {
                    gamePlayingTimer = GamePlayingTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0.0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }
    
    public bool IsCountdownToStartActive() {
        return state == State.CountDownToStart;
    }
    
    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }
    
    public bool IsGameOver() {
        return state == State.GameOver;
    }
    
    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / GamePlayingTimerMax);
    }
    
    private void TogglePauseGame() {
        bIsGamePause = !bIsGamePause;
        if (bIsGamePause) {
            Time.timeScale = 0.0f;
        } else {
            Time.timeScale = 1.0f;
        }
        
    }
}
