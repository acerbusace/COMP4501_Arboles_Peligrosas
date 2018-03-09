using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{

    void Start()
    {
        unitName = "Player";
        unitHealth = 100f;
        speed = 5f;
        gatherSpeed = 1f;
        gatherDistance = 5f;

        sfInfo.info = new Dictionary<string, string>();
    }

    public override void updateSFInfo()
    {
        HelperFunctions.addToDict(sfInfo.info, "Unit", unitName);
        HelperFunctions.addToDict(sfInfo.info, "Health", ((int)unitHealth).ToString());
        HelperFunctions.addToDict(sfInfo.info, "Speed", ((int)speed).ToString());
    }
}

