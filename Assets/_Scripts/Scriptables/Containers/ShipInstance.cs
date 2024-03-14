using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShipInstance : MonoBehaviour {

    [SerializeField] private ShipStats _stats;
    
    [SerializeField] private int currentHP;
    [SerializeField] private List<JetInstance> jetModules = new();


    public void SetupShip(ShipStats baseStats) {
        // this check probably not necessary?
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

    public List<JetInstance> GetJets() {
        return jetModules;
    }

    public int GetMaxTravelDistance() {
        int travelDistance = 0;
        foreach (var jet in jetModules) {
            if (jet.ModuleEnabled()) {
                travelDistance += jet.GetMoveDistance();
            }
        }
        return travelDistance;
    }
}
