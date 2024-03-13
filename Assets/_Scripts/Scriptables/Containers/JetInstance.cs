using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetInstance {

    [SerializeField] private JetStats _stats;

    [SerializeField] private bool ModuleEnabled;
    
    public JetInstance(JetStats baseStats) {
        _stats = baseStats;
        ModuleEnabled = false;
    }
}
