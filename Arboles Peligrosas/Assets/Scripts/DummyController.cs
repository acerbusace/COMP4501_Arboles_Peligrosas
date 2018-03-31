using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour {

    private float rotationSpeed;

	// Use this for initialization
	void Start () {
        rotationSpeed = 90f;
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            transform.position = HelperFunctions.hitToVector(hit, gameObject);

        if (Input.GetKey(KeyCode.R)) {
            Debug.Log("rotation object");
            transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
        }
    }

}
