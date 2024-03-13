using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipStats", menuName = "SO/ShipStats", order = 0)]
public class ShipStats : ScriptableObject {

    [Header("Description")]
    public string Name;
    public string Description;

    [Header("Stats")]
    public HullStats HullStats;
    public int MaxJets;
    public List<JetStats> JetStats;

}