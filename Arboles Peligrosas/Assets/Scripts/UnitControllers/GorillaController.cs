using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaController : Enemy {

	// Use this for initialization
	void Start () {
        unitName = "Gorilla";
        unitHealth = 250f;
        speed = 75f;
        
        rotationSpeed = 4f;
        maxVelocity = 15f;
    }
}
