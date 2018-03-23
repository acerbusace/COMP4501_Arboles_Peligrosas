using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Action
{
    private Vector3 destination;
    GameObject gameObject;
    Rigidbody rigidbody;
    float speed;
    float maxVelocity;
    float rotationSpeed;

    //Flock flock;
    //Seek seek;
    //Wander wander;

    bool first;
    bool finished;

    public Movement(GameObject gb, Vector3 dest, float s, float mS, float rS)
    {
        gameObject = gb;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        destination = dest;
        speed = s;
        maxVelocity = mS;
        rotationSpeed = rS;

        first = true;
        finished = false;
    }

    public override void start()
    {
        Vector3 dir = destination - rigidbody.position;

        if (HelperFunctions.rotateTowardsVelocity(rigidbody.gameObject, rotationSpeed, dir))
        {
            rigidbody.AddForce(dir.normalized * speed, ForceMode.Acceleration);
            
            if (rigidbody.velocity.magnitude > maxVelocity) rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        } else {
            rigidbody.velocity = Vector3.zero;
        }
    }

    public override void update()
    {
        Debug.Log("this should be getting called");
        if(!finished) start();

        if(Vector3.Distance(rigidbody.position, destination) < 1f) {
            finished = true;
        }
    }

    public override bool isFinished()
    {
        return finished;
    }

    

    public override Vector3 getDestination() { return destination; }
}