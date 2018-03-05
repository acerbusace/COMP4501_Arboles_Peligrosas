using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 destination;
    private float speed;
    private Lerp movement;

	// Use this for initialization
	void Start ()
    {
        speed = 5.0f;
        destination = transform.position;
        movement = new Lerp(transform.position, transform.position, speed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) //left mouse button
            setDestination(Camera.main.ScreenPointToRay(Input.mousePosition));

        movePlayer();
	}

    void movePlayer()
    {
        if (transform.position == destination) return;
        
        transform.position = movement.getPosition();
    }

    void setDestination(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point + new Vector3(0, 0.5f, 0);
        }

        movement = new Lerp(transform.position, destination, speed);
    }
}

class Lerp
{
    private Vector3 start, end;
    private float distance, timeToEnd, currTime;

    public Lerp(Vector3 s, Vector3 e, float speed)
    {
        start = s;
        end = e;
        distance = Vector3.Distance(s, e);
        timeToEnd = distance / speed;
        currTime = 0;
    }

    public Vector3 getPosition()
    {
        currTime += Time.deltaTime;

        if (currTime >= timeToEnd)
            currTime = timeToEnd;

        return Vector3.Lerp(start, end, currTime/timeToEnd);
    }
}
