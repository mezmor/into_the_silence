using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit; // All actions have a unit on which they act
    protected bool isActive; // is this action currently running
    protected Action onActionComplete; // callback for when the action is complete

    protected virtual void Awake() {
        this.isActive = false;
        this.unit = GetComponent<Unit>();
        Debug.Log("[AWAKE] [Unit: " + this.unit.gameObject.name + "] [Action: " + this.GetActionName() + "]");
    }

    public abstract String GetActionName();

    public abstract void TakeAction(GameObject target, Action onActionComplete);
}
