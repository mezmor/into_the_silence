using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAction : BaseAction {
    private Vector2 direction;
    private float orbitRadius;
    private float orbitVelocity;

    private GameObject orbitTarget;

    // Update is called once per frame
    private void FixedUpdate() {
        if (!isActive) { return; }

        //GETTING DIRECTION
        Vector3 dirToTarget = (orbitTarget.transform.position - transform.position).normalized;
        Vector2 perpendicularVector = Vector2.Perpendicular(dirToTarget).normalized;
        float distance = Vector3.Distance(transform.position, orbitTarget.transform.position);

        // if we're not within the orbit radius, fly towards orbit radius
        // if (distance > orbitRadius) {
        //     this.unit.GetComponent<Rigidbody2D>().AddForce(dirToTarget * PlayerController.Instance.GetAvailableThrust()); 
        // } else if (distance < orbitRadius) { // if we're within the orbit radius, add forces to orbit
        //     this.unit.GetComponent<Rigidbody2D>().AddForce(-dirToTarget * PlayerController.Instance.GetAvailableThrust()); 
        // } else {
        //     this.unit.GetComponent<Rigidbody2D>().AddForce(dirToTarget * (Mathf.Pow(orbitVelocity, 2f) / orbitRadius)); 
        // }

        if (distance >= orbitRadius) {
            this.unit.GetComponent<Rigidbody2D>().AddForce(dirToTarget * (Mathf.Pow(orbitVelocity, 2) / orbitRadius));
        }

        // if we're within some distance threshhold of the orbit, start applying angular force until we're at the target velocity
        // if (this.unit.GetSpeed() < orbitVelocity) {
        //     this.unit.GetComponent<Rigidbody2D>().AddForce(perpendicularVector * RealTimeMovementPlayerController.Instance.GetAvailableThrust());
        // }
        

        // Debug.DrawLine(gameObject.transform.position, orbitTarget.transform.position, Color.yellow);
        // Debug.DrawRay(transform.position, dirToTarget, Color.red);
		// Debug.DrawRay(transform.position, perpendicularVector, Color.green);	

        // Debug.Log("DISTANCE: " + distance);
        // Debug.Log(" RADIUS : " + orbitRadius);
        // Debug.Log("  REAL V: " + this.unit.GetSpeed());
        // Debug.Log("TARGET V: " + orbitVelocity);
    }

    public override void TakeAction(GameObject target, Action onActionComplete) {
        // We can't orbit ourselves, and we can't do anything more if we're told to orbit our current target again
        if (this.gameObject == target || orbitTarget == target) {
            return;
        }

        this.TakeAction(target, 10f, 30f, onActionComplete);
    }

    public void TakeAction(GameObject target, float orbitVelocity, float orbitDistance, Action onActionComplete) {
        this.orbitTarget = target;
        this.orbitVelocity = orbitVelocity;
        this.orbitRadius = orbitDistance;
        this.onActionComplete = onActionComplete;
        isActive = true;

        Debug.Log("[ACTION] [ORBIT] START: " + this.unit.gameObject.name);

        // direction = orbitTarget.transform.position - transform.position;
        // transform.right = direction;
        // this.unit.GetComponent<Rigidbody2D>().AddForce(transform.up * initialImpulse);

        // direction = (orbitTarget.transform.position - transform.position).normalized;
        // this.unit.GetComponent<Rigidbody2D>().AddForce(direction * PlayerController.Instance.GetAvailableThrust());
    }

    public override string GetActionName() {
        return "ORBIT";
    }
}
