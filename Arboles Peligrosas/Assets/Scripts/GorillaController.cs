using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaController : MonoBehaviour {

    int health;

	// Use this for initialization
	void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            // Get rid of Gorilla
        }
    }
}
