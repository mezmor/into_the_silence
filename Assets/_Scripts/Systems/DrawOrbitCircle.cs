using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrbitCircle : Singleton<DrawOrbitCircle> {

    [SerializeField] private LineRenderer circleRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float radius;

    private void Start() {
        circleRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward * -15f * Time.deltaTime);
    }

    void DrawCircle() {
        circleRenderer.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; currentStep++) {
            float circumferenceProgress = (float) currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = radius * Mathf.Cos(currentRadian);
            float yScaled = radius * Mathf.Sin(currentRadian);

            float x = xScaled; //* radius;
            float y = yScaled; // * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    public void ShowCircle(GameObject anchor, float radius) {
        gameObject.transform.position = anchor.transform.position;
        gameObject.transform.SetParent(anchor.transform);

        this.radius = radius;

        DrawCircle();

        gameObject.SetActive(true);
    }

    public void HideCircle() {
        circleRenderer.positionCount = 0;
        gameObject.SetActive(false);
    }

}
