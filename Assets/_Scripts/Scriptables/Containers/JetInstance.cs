using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class JetInstance {

    [SerializeField] private JetStats _stats;

    [SerializeField] private bool moduleEnabled;
    
    public JetInstance(JetStats baseStats) {
        _stats = baseStats;
        moduleEnabled = false;
    }
    
    public void EnableModule() {
        moduleEnabled = true;
    }

    public void DisableModule() {
        moduleEnabled = false;
    }

    public void ToggleModule() {
        moduleEnabled = !moduleEnabled;
    }

    public bool ModuleEnabled() {
        return moduleEnabled;
    }
}
