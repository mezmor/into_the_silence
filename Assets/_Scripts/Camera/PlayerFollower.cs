using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {
    private GameObject player;

    // Update is called once per frame
    void Update() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = player.transform.position;
    }
}
