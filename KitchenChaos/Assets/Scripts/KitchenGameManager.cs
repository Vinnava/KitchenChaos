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
    private float CountDownToStartTimer = 3.0f;
    private float GamePlayingTimer = 10.0f;

    private void Awake() {
        state = State.WaitingToStart;
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
                CountDownToStartTimer -= Time.deltaTime;
                if (CountDownToStartTimer < 0.0f) {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0.0f) {
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
}
