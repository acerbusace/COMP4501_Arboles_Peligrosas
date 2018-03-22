using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

    Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        rigidbody.velocity = new Vector3(1f, 0f, 0f);
        print("velocity: " + rigidbody.velocity + " > " + Time.deltaTime);
	}
}
