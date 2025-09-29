using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
    
    public static KitchenGameManager Instance{ get; private set;}
    
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
                }
                break;
            case State.CountDownToStart:
                CountDownToStartTimer -= Time.deltaTime;
                if (CountDownToStartTimer < 0.0f) {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0.0f) {
                    state = State.GameOver;
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
}
