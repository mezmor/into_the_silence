using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrbitDialogUI : Singleton<OrbitDialogUI>
{
    [SerializeField] private TMP_InputField distanceInput;
    [SerializeField] private TMP_InputField speedInput;
    [SerializeField] private Button confirmButton;

    private GameObject anchor;

    protected override void Awake() {
        base.Awake();
        Hide();
    }

    private void Update() {
        if (anchor != null) {
            Vector3 anchorPos = anchor.transform.position;
            gameObject.transform.position = new Vector3(anchorPos.x, anchorPos.y, anchorPos.z);
        }
    }

    private void Start() {
        UnitSelectionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    }

    public void ShowDialog(GameObject player, GameObject orbitTarget, string placeholderDistance, string placeholderSpeed, Action confirmAction) {
        Debug.Log("Open Orbit Dialog | Distance: " + placeholderDistance + ", Speed:" + placeholderSpeed);

        gameObject.SetActive(true);
        
        distanceInput.text = placeholderDistance;
        speedInput.text = placeholderSpeed;

        // confirmButton.onClick.AddListener(() => {
        //     Hide();
        //     DrawOrbitCircle.Instance.ShowCircle(orbitTarget, float.Parse(distanceInput.text));
        //     player.GetComponent<OrbitAction>().TakeAction(orbitTarget, float.Parse(speedInput.text), float.Parse(distanceInput.text), null); // call the player's orbit action
        //     confirmAction();
        // });
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e) {
        Hide();
    }

}
