using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementModeSystem : Singleton<MovementModeSystem> {

    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject playerCloneObject;
    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float radius;

    private bool modeActive = false;

    private void Start() {
        circleRenderer.positionCount = 0;
        UnitSelectionSystem.Instance.OnSelectedUnitChanged += SelectedUnitChangedHandler;
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        TryHandleMovementModeEnablement();

        if (modeActive) MovementModeGo();
    }

    private void MovementModeGo() {
        // We clamp the playerClone's position to within the radius around the player.
        Vector2 center = playerObject.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 offset = mousePos - center;
        Vector2 clampedOffset = Vector2.ClampMagnitude(offset, radius);

        playerCloneObject.transform.position = center + clampedOffset;

        // left click to confirm
        // confirm sets the jet module's task to move to the clone's position
        // right click to cancel
    }

    private void TryHandleMovementModeEnablement() {
        // Right click the selected object
        if (Input.GetMouseButtonDown(1) && UnitSelectionSystem.Instance.GetSelectedUnit() is var selectedUnit && selectedUnit != null) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // The right-clicked unit must be the selected unit (the player)
            if (hit.collider != null
                && selectedUnit.gameObject == hit.transform.gameObject
                && selectedUnit.gameObject == playerObject) {
                    EnableMovementMode();
            }
        }
    }

    private void EnableMovementMode() {
        this.radius = 3f; // Ultimately, we get the maximum movement distance from the player's jets.

        ShowCircle(playerObject);
        ShowPlayerClone(playerObject);
        this.modeActive = true;
    }

    private void ShowCircle(GameObject anchor) {
        gameObject.transform.position = anchor.transform.position;
        gameObject.transform.SetParent(anchor.transform);
        DrawCircle();
    }

    private void ShowPlayerClone(GameObject playerObject) {
        playerCloneObject.SetActive(true);
    }

    private void DrawCircle() {
        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++) {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = radius * Mathf.Cos(currentRadian);
            float yScaled = radius * Mathf.Sin(currentRadian);

            float x = xScaled; //* radius;
            float y = yScaled; // * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    private void DisableMovementMode() {
        this.modeActive = false;

        HidePlayerClone();
        HideCircle();
    }

    private void HidePlayerClone() {
        playerCloneObject.SetActive(false);
    }

    private void HideCircle() {
        circleRenderer.positionCount = 0;
    }

    private void SelectedUnitChangedHandler(object sender, EventArgs e) {
        DisableMovementMode();
    }
}
