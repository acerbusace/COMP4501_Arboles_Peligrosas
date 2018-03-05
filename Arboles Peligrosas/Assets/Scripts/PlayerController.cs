using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 destination;
    private float speed;

	// Use this for initialization
	void Start ()
    {
        destination = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) //left mouse button
            destination = setDestination(Camera.main.ScreenPointToRay(Input.mousePosition));

        movePlayer();
	}

    void movePlayer()
    {
        if (this.transform.position == destination) return;

        this.transform.SetPositionAndRotation(destination, new Quaternion());
    }

    Vector3 setDestination(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return (hit.point + new Vector3(0, 0.5f, 0));
        }

        return new Vector3(); //shouldn't happen
    }
}
