using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTurnManager : Singleton<PlayerTurnManager> {

    [SerializeField] private GameObject playerObject;

    // Start is called before the first frame update
    void Start() {
        UnitSelectionSystem.Instance.OnSelectedUnitChanged += UnitSelectionSystem_OnSelectedUnitChanged;
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) { // returns true if mouse is over a UI element
            return;
        }
    }

    private void UnitSelectionSystem_OnSelectedUnitChanged(object sender, EventArgs e) {
        Unit selectedUnit = UnitSelectionSystem.Instance.GetSelectedUnit();

        if (selectedUnit == null) { return; }

        if (selectedUnit.gameObject != playerObject) { return; }

        // now, selectedUnit.gameObject == playerObject
    }
}
