using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JetStats", menuName = "SO/JetStats")]
public class JetStats : ScriptableObject {

    [Header("Description")]
    public string Name;
    public string Description;

    [Header("Stats")]
    public int MoveDistance;
    public int EnergyConsumption;
}