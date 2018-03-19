using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : Flocker {

	// Use this for initialization
	void Start () {
        unitName = "Tiger";
        unitHealth = 100f;
        speed = 30f;

        seekRange = 50f;
        wanderRange = 15f;
        rotationSpeed = 4f;
        maxVelocity = 9f;

        sfInfo.info = new Dictionary<string, string>();

        //queueMove(new Vector3(1000, 0, 1000));
    }

    public override void updateSFInfo()
    {
        HelperFunctions.addToDict(sfInfo.info, "Unit", unitName);
        HelperFunctions.addToDict(sfInfo.info, "Health", ((int)unitHealth).ToString());
        if (state == EnemyState.Seeking)
            HelperFunctions.addToDict(sfInfo.info, "State", "Seeking");
        else if (state == EnemyState.Wandering)
            HelperFunctions.addToDict(sfInfo.info, "State", "Wandering");
        else
            HelperFunctions.addToDict(sfInfo.info, "State", "");
    }
}
