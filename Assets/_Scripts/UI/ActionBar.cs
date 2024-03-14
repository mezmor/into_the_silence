using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {

    private void Awake() {
        GameManager.OnBeforeStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state) {
        gameObject.SetActive(state == GameState.PrepareTurn);
    }
}
