using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareMoveAction : BaseAction {

    private void FixedUpdate() {
        if (!isActive) { return; }
        
    }

    public override string GetActionName() {
        return "PREPARE MOVE";
    }

    public override void TakeAction(GameObject target, Action onActionComplete) {
        throw new NotImplementedException();
    }
}