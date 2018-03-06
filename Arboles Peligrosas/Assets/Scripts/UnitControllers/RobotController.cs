using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour, Selectable {

    private Vector3 destination, lastPosition;
    private float speed;
    private Lerp movement;
    private UI_SelectedFrame sfInfo;

	void Start ()
    {
        speed = 5f;
        destination = transform.position;
        movement = new Lerp(transform.position, transform.position, speed);
        sfInfo.name = "Robot";
        sfInfo.health = 100;
	}
    
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        lastPosition = transform.position;
        transform.position = movement.getPosition();
    }

    void OnCollisionEnter(Collision col)
    {
        transform.position = lastPosition;

        if (col.gameObject.tag != "Ground")
            movement = new Lerp(transform.position, transform.position, speed);
    }

    public void setDestination(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
            destination.y = 0.5f; //terrain.y @ destination.x & destination.z + playerSize.y/2
        }

        movement = new Lerp(transform.position, destination, speed);
    }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
}
