using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{    
    //public static DeathWall Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this){
    //        Destroy(this);
    //    }
    //    else{
    //        Instance = this;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // check if the collision is the player
        other.TryGetComponent<PlayerController>(out PlayerController checkPlayer);

        if (checkPlayer != null)
        {
            checkPlayer.Respawn();
        }
    }
}
