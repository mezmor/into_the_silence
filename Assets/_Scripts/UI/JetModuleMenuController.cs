using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetModuleMenuController : MonoBehaviour {

    [SerializeField] GameObject moduleSelectorUi;
    [SerializeField] GameObject moduleSelectorIconPrefab;

    private bool visible;
    private bool instantiated;

    // Start is called before the first frame update
    void Start() {
        visible = false;
        instantiated = false;
        moduleSelectorUi.SetActive(false);
    }

    // UI onclick
    public void ToggleSelector() {
        if (!instantiated) {
            InstantiateIcons();
            instantiated = true;
        }
        visible = !visible;
        moduleSelectorUi.SetActive(visible);

        if(visible) {
            MovementModeSystem.Instance.EnableMovementMode();
        } else {
            MovementModeSystem.Instance.DisableMovementMode();
        }
    }

    public void EnableSelector() {
        if (!instantiated) {
            InstantiateIcons();
            instantiated = true;
        }
        visible = true;
        moduleSelectorUi.SetActive(true);
    }

    public void DisableSelector() {
        visible = false;
        moduleSelectorUi.SetActive(false);
    }

    public void InstantiateIcons() {
        // get the ui container for the icons
        var iconContainer = moduleSelectorUi.transform.Find("IconContainer");
        // Get player's equipped jets
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        ShipInstance shipInstance = playerObject.GetComponent<ShipInstance>();
        foreach (var jetInstance in shipInstance.GetJets()) {
            GameObject selectorIcon = Instantiate(moduleSelectorIconPrefab, iconContainer);
            selectorIcon.GetComponent<JetModuleSelectorIcon>().SetModule(jetInstance);
        }
    }

}
