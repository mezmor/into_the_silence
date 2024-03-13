using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HullStats", menuName = "SO/HullStats", order = 1)]
public class HullStats : ScriptableObject {

    [Header("Description")]
    public string Name;
    public string Description;

    [Header("Stats")]
    public int MaxHP;
}