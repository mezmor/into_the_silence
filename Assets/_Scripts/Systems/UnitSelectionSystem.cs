using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelectionSystem : Singleton<UnitSelectionSystem> {
    [SerializeField] private GameObject unitTargetedPrefab;
    [SerializeField] private LayerMask selectableLayerMask;
    [SerializeField] private LayerMask uiLayerMask;
    [SerializeField] private GameObject playerGameObject;
    private GameObject targetReticleObj;

    public event EventHandler OnSelectedUnitChanged;

    private Unit selectedUnit;

    // Start is called before the first frame update
    void Start() {
        targetReticleObj = Instantiate(unitTargetedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        OnSelectedUnitChanged += DrawSelectedReticle;
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) { // returns true if mouse is over a UI element
            return;
        }

        // we don't want to select things if the movement mode is active
        if (MovementModeSystem.Instance.IsActive()) {
            return;
        }

        if (TryHandleUnitSelection()) {
            return;
        }
    }

    private bool TryHandleUnitSelection() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D unitHit = Physics2D.Raycast(mousePos2D, Vector2.zero, float.MaxValue, selectableLayerMask);

            // clicked into nothingness with nothing selected, do nothing
            if (unitHit.collider == null && selectedUnit == null) {
                return false;
            }

            // we clicked into nothingness with something selected, unselect
            if (unitHit.collider == null && selectedUnit != null) {
                SetSelectedUnit(null);
                return true;
            }

            // we click a unit
            if (unitHit.collider != null && unitHit.transform.TryGetComponent<Unit>(out Unit unit)) {
                if (unit == selectedUnit) {
                    return false;
                }
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit) {
        Debug.Log("SetSelectedUnit|: " + unit?.gameObject.name);
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit() {
        return this.selectedUnit;
    }

    private void DrawSelectedReticle(object sender, EventArgs e) {
        if (selectedUnit != null) {
            targetReticleObj.transform.position = selectedUnit.transform.position;
            targetReticleObj.transform.SetParent(selectedUnit.transform);
            targetReticleObj.SetActive(true);
        } else {
            targetReticleObj.SetActive(false);
        }
    }

}
