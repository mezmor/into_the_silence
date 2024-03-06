using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RealTimeMovementPlayerController : Singleton<RealTimeMovementPlayerController> {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float m_Thrust; // controlled by engine strength
    [SerializeField] private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) { // returns true if mouse is over a UI element
            return;
        }

        TryHandleOrbitAction();
    }

    void FixedUpdate() {
        HandleMovementInput();
    }

    public float GetAvailableThrust() {
        return m_Thrust;
    }

    private void HandleMovementInput() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        bool boosting = Input.GetKey(KeyCode.LeftShift);
        float boostMod = boosting ? 5 : 1;

        Vector2 movement = new Vector3(moveHorizontal, moveVertical);
        rb.AddForce(movement * m_Thrust * boostMod);

        // Debug.Log("Player velocity: " + rb.velocity);

        // Maybe better to update the UI element in another script
        // textMeshPro.text = GetComponent<Unit>().GetSpeed() + " u/s";
    }

    /*
    * Handle right clicks to orbit
    */ 
    private void TryHandleOrbitAction() {
        // Right click a selected object to orbit it with the player
        if (Input.GetMouseButtonDown(1) && UnitSelectionSystem.Instance.GetSelectedUnit() is var selectedUnit && selectedUnit != null) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // The right-clicked unit must be the selected unit
            if (hit.collider != null && selectedUnit.gameObject == hit.transform.gameObject) {
                OrbitDialogUI.Instance.ShowDialog(
                    gameObject,
                    hit.transform.gameObject,
                    Vector3.Distance(gameObject.transform.position, hit.transform.position).ToString(),
                    "5",
                    () => {}
                );

            }
        }
    }
}
