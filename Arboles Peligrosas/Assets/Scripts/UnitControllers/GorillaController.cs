using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GorillaController : Enemy {

	// Use this for initialization
	void Start () {
        unitName = "Gorilla";
        unitHealth = 250f;

        agent = GetComponent<NavMeshAgent>();
    }
}
