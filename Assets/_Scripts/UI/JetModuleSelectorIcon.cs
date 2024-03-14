using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JetModuleSelectorIcon : MonoBehaviour {
    private JetInstance jetModule;

    void Start() {
        SetIconColorOff();
    }

    public void SetModule(JetInstance jetRef) {
        jetModule = jetRef;
        SetIconColor();
    }

    public void EnableModule() {
        jetModule.EnableModule();
        SetIconColor();
    }

    // UI onclick
    public void ToggleModule() {
        jetModule.ToggleModule();
        SetIconColor();
    }

    private void SetIconColor() {
        if (jetModule.ModuleEnabled()) {
            gameObject.GetComponent<Image>().color = new Color32(24, 128, 24, 255);
        } else {
            SetIconColorOff();
        }
    }

    private void SetIconColorOff() {
        // gameObject.GetComponent<Image>().color = new Color(156, 0, 0, 255);
        gameObject.GetComponent<Image>().color = new Color32(24, 25, 26, 255);
    }

}
