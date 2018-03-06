using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour {

    float x;
    float y;
    float z;
    Vector3 pos;
    int health;

    // Use this for initialization
    void Start () {
        x = Random.Range(-250, 260);
        y = 0;
        z = Random.Range(-250, 260);
        health = 0;
        pos = new Vector3(x, y, z);
        transform.position = pos;

    }
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            // Get rid of tree
        }
	}
}
