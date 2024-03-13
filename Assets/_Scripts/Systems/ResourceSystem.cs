using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem> {

    public List<ShipStats> ShipStats { get; private set; }
    public List<HullStats> HullStats { get; private set; }
    public List<JetStats> JetStats { get; private set; }

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        ShipStats = Resources.LoadAll<ShipStats>("ScriptableObjects/Ships").ToList();
        HullStats = Resources.LoadAll<HullStats>("ScriptableObjects/Hulls").ToList();
        JetStats = Resources.LoadAll<JetStats>("ScriptableObjects/Jets").ToList();
    }
}
