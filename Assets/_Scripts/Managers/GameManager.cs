using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    protected override void Awake() {
        base.Awake();
        ChangeState(GameState.PrepareTurn); // Right into game
    }

    void Start() {
        // ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState) {
        if (State == newState) return;

        OnBeforeStateChanged?.Invoke(newState);
        
        Debug.Log($"HANDLING NEW GAMESTATE: {newState}");

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.SaveSelect:
                HandleSaveSelect();
                break;
            case GameState.NewGameFlow:
                break;
            case GameState.RecruitHero:
                break;
            case GameState.FirmLobby:
                break;
            case GameState.PrepareTurn:
                break;
            case GameState.PerformTurn:
                break;
            case GameState.Returned:
                break;
            case GameState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private void HandleStarting() {
        ChangeState(GameState.SaveSelect);
    }

    private void HandleSaveSelect() {
        ChangeState(GameState.NewGameFlow);
    }
}

public enum GameState {
    Placeholder,
    Starting,
    SaveSelect,
    NewGameFlow,
    RecruitHero,
    FirmLobby,
    PrepareTurn,
    PerformTurn,
    Returned,
    Lost,
}