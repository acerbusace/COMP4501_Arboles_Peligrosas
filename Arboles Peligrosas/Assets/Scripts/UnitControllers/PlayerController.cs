using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{

    void Start()
    {
        GetComponent<Movement>().setSpeed(5f);
        sfInfo.name = "Player";
        sfInfo.health = 100f;
    }

}

