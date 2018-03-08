using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{

    void Start()
    {
        sfInfo.name = "Player";
        sfInfo.health = 100f;

        unitName = "Player";
        unitHealth = 100f;

        sfInfo.info = new Dictionary<string, string>();
        sfInfo.info.Add("Unit", unitName);
        sfInfo.info.Add("Health", ((int)unitHealth).ToString());
        sfInfo.info.Add("Speed", ((int)GetComponent<Movement>().getSpeed()).ToString());
    }

}

