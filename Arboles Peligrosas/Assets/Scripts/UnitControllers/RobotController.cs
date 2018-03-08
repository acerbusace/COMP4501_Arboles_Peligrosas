using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : Actor
{

	void Start ()
    {
        sfInfo.name = "Robot";
        sfInfo.health = 100f;

        unitName = "Robot";
        unitHealth = 100f;

        sfInfo.info = new Dictionary<string, string>();
        sfInfo.info.Add("Unit", unitName);
        sfInfo.info.Add("Health", ((int)unitHealth).ToString());
        sfInfo.info.Add("Speed", ((int)GetComponent<Movement>().getSpeed()).ToString());
	}
    
}
