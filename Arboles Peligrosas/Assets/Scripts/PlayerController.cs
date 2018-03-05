using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Selectable
{
    private Vector3 destination;
    private float speed;
    private Lerp movement;
    private UI_SelectedFrame sfInfo;

	void Start ()
    {
        speed = 5f;
        destination = transform.position;
        movement = new Lerp(transform.position, transform.position, speed);
        sfInfo.name = "Player";
        sfInfo.health = 100f;
	}

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

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
}

