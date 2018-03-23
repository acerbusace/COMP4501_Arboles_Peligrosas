using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerController : Flocker {

    SteeringController steeringController;

	// Use this for initialization
	void Start () {
        unitName = "Tiger";
        unitHealth = 100f;
        speed = 100f;
        
        rotationSpeed = 4f;
        maxVelocity = 7.5f;

        sfInfo.info = new Dictionary<string, string>();

        steeringController = GetComponent<SteeringController>();
        steeringController.addDestination(new Vector3(50f, 0f, 0f));
        steeringController.addDestination(new Vector3(100f, 0f, -50f));
        steeringController.addDestination(new Vector3(150f, 0f, 50f));
        steeringController.addDestination(new Vector3(200f, 0f, 0));
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
        HelperFunctions.addToDict(sfInfo.info, "Leader", isLeader.ToString());
    }
}
