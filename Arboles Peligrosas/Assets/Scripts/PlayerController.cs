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
        movePlayer();
	}

    void movePlayer()
    {
        //GetComponent<Rigidbody>().AddForce(-(transform.position - destination) * speed);
        transform.position = movement.getPosition();
    }

    public void setDestination(Ray ray)
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

