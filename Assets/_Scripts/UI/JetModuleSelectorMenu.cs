using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetModuleSelectorMenu : MonoBehaviour {

    [SerializeField] GameObject moduleSelectorUi;

    private bool visible;

    // Start is called before the first frame update
    void Start() {
        visible = false;
        moduleSelectorUi.SetActive(false);
    }

    public void ToggleSelector() {
        visible = !visible;
        moduleSelectorUi.SetActive(visible);
    }
}
