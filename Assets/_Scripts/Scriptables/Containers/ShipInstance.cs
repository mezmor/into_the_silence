using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInstance {

    [SerializeField] private ShipStats _stats;
    
    [SerializeField] private int currentHP;
    [SerializeField] private List<JetInstance> jetModules;


    public ShipInstance(ShipStats baseStats) {
        if (baseStats == null) {
            throw new ArgumentNullException();
        }

        this._stats = baseStats;

        // Hull determines initial HP - maybe we just track this on a HullInstance?
        currentHP = baseStats.HullStats.MaxHP;

        for (int i = 0; i < baseStats.MaxJets; i++) {
            jetModules.Add(new JetInstance(_stats.JetStats[i]));
        }
    }
}
