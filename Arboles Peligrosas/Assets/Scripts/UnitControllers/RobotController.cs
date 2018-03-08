using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : Actor
{

	void Start ()
    {
        GetComponent<Movement>().setSpeed(4f);
        sfInfo.name = "Robot";
        sfInfo.health = 100f;
	}
    
}
