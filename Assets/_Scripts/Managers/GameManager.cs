using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : StaticInstance<GameManager> {

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private MovementModeSystem movementModePrefab;


    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    protected override void Awake() {
        base.Awake();
        // ChangeState(GameState.PrepareTurn); // Right into game
        ChangeState(GameState.LoadSilence);
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
            case GameState.LoadSilence:
                LoadAllTheThings();
                break;
            case GameState.PrepareTurn:
                break;
            case GameState.ExecuteTurn:
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

    private void LoadAllTheThings() {
        Debug.Log("LOADING START");
        // Load map
        // Probably spawn all the things on the map

        // Load player
        Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);

        // SYSTEMS: ENGAGE
        Instantiate(movementModePrefab, new Vector2(0, 0), Quaternion.identity);

        Debug.Log("LOADING FINISH");
        ChangeState(GameState.PrepareTurn);
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
    LoadSilence,
    PrepareTurn,
    ExecuteTurn,
    Returned,
    Lost,
}