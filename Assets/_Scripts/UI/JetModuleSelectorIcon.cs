using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JetModuleSelectorIcon : MonoBehaviour {
    private bool moduleEnabled;

    void Start() {
        moduleEnabled = false;
        SetIconColor();
    }

    public void ToggleModule() {
        moduleEnabled = !moduleEnabled;
        SetIconColor();
    }

    private void SetIconColor() {
        if(moduleEnabled) {
            gameObject.GetComponent<Image>().color = new Color32(24, 128, 24, 255);
        } else {
            // gameObject.GetComponent<Image>().color = new Color(156, 0, 0, 255);
            gameObject.GetComponent<Image>().color = new Color32(24, 25, 26, 255);
        }
    }
}
