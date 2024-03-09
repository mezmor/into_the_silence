using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementModeSystem : Singleton<MovementModeSystem> {
    /*
    * states:
    * - not in movement mode - no planned movement --> dont draw shit
    * - not in movement mode - planned movement    --> draw transparent clone (at planned movement)
    * - in movement mode     - no planned movement --> draw dynamic transparent clone & ring, no planned movement to draw
    * - in movement mode     - planned movement    --> draw dynamic transparent clone & ring, do not draw planned movement
    *
    * state changes & actions are:
    * not in movement mode --( right clicking player / clicking movement action button )--> movement mode --> no change to planned action
    *        movement mode --( right click cancel  )-->                not in movement mode                --> no change to planned action
    *        movement mode --( left click confirm  )-->                not in movement mode                --> cancel planned action, plan this action 
    *        movement mode --( shift + right click )-->                not in movement mode                --> cancel planned action if one
    * not in movement mode --( cancel button       )-->                not in movement mode                --> cancel planned action if one [TODO]
    */

    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject playerCloneObject;
    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float radius;

    private bool modeActive = false;
    private bool actionPlanned;
    private Vector2 plannedActionPosition;

    private void Start() {
        circleRenderer.positionCount = 0;
        UnitSelectionSystem.Instance.OnSelectedUnitChanged += SelectedUnitChangedHandler;
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (modeActive) MovementModeGo();
        if (!modeActive) TryHandleMovementModeEnablement();
    }

    private void MovementModeGo() {
        // We clamp the playerClone's position to within the radius around the player.
        Vector2 center = playerObject.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 offset = mousePos - center;
        Vector2 clampedOffset = Vector2.ClampMagnitude(offset, radius);

        playerCloneObject.transform.position = center + clampedOffset;
        
        // left click to confirm
        if (Input.GetMouseButtonDown(0)) {
            ConfirmMovement();
        }
        
        // right click to exit movement mode without removing our planned action
        if (Input.GetMouseButtonDown(1)) {
            // if holding shift, we'll cancel the planned action
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                CancelMovement();
            } else {
                // not holding shift, just exit mode
                DisableMovementMode();
            }
        }


    }

    private void ConfirmMovement() {
        plannedActionPosition = playerCloneObject.transform.position;

        // TODO: register movement action using plannedActionPosition

        actionPlanned = true;
        DisableMovementMode();
    }

    private void CancelMovement() {
        actionPlanned = false;

        // TODO: remove registered move action

        DisableMovementMode();
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

        ShowDynamicPlayerClone();
        ShowCircle();

        modeActive = true;
    }

    private void DisableMovementMode() {
        this.modeActive = false;

        TryShowStaticPlayerClone();
        HideCircle();
    }

    private void ShowDynamicPlayerClone() {
        // check the execution queue for the movement position
        playerCloneObject.SetActive(true);
    }

    private void TryShowStaticPlayerClone() {
        if (actionPlanned) {
            // TODO: check execution queue for move action
            playerCloneObject.transform.position = plannedActionPosition;
            playerCloneObject.SetActive(true);
        } else {
            HidePlayerClone();
        }
    }

    private void HidePlayerClone() {
        playerCloneObject.SetActive(false);
    }

    private void ShowCircle() {
        gameObject.transform.position = playerObject.transform.position;
        gameObject.transform.SetParent(playerObject.transform);
        DrawCircle();
    }

    private void HideCircle() {
        circleRenderer.positionCount = 0;
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

    private void SelectedUnitChangedHandler(object sender, EventArgs e) {
        DisableMovementMode();
    }

    public bool IsActive() {
        return modeActive;
    }
}
