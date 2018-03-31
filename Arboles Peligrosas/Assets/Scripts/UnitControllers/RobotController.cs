using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : Friendly
{

	void Start ()
    {
        unitName = "Robot";
        unitHealth = 75f;
        speed = 75f;
        gatherSpeed = 0.5f;
        gatherDistance = 5f;
        rotationSpeed = 12f;
        maxVelocity = 7.5f;

        sfInfo.info = new Dictionary<string, string>();
	}
    public override void updateSFInfo()
    {
        base.updateSFInfo();
        HelperFunctions.addToDict(sfInfo.info, "Speed", ((int)speed).ToString());
    }
}
