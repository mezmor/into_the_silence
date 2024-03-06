using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotation : MonoBehaviour {
    void Update() {
        if (gameObject.transform.rotation != Quaternion.identity) {
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
