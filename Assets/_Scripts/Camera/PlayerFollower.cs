using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
