using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : Friendly
{

	void Start ()
    {
        unitName = "Robot";
        unitHealth = 100f;
        speed = 20f;
        gatherSpeed = 0.5f;
        gatherDistance = 5f;
        rotationSpeed = 12f;
        maxVelocity = 7.5f;

        sfInfo.info = new Dictionary<string, string>();
	}
    public override void updateSFInfo()
    {
        HelperFunctions.addToDict(sfInfo.info, "Unit", unitName);
        HelperFunctions.addToDict(sfInfo.info, "Health", ((int)unitHealth).ToString());
        HelperFunctions.addToDict(sfInfo.info, "Speed", ((int)speed).ToString());
    }
}
