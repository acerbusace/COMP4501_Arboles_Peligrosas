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

        if (first)
        {
            rigidbody.velocity = Vector3.zero;
            first = false;
        }

        if (HelperFunctions.rotateTowardsVelocity(rigidbody.gameObject, rotationSpeed, dir))
        {
            rigidbody.velocity += dir.normalized * speed * Time.deltaTime;

            Flocker flocker = gameObject.GetComponent<Flocker>();
            //if (flocker)
            //{
            //    rigidbody.velocity += flocker.getFlockVector() * speed * Time.deltaTime;
            //}

            if (rigidbody.velocity.magnitude > maxVelocity)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
            }
        }
    }

    public override void update()
    {
        if (Vector3.Distance(rigidbody.position, destination) < 5f)
        {
            rigidbody.velocity *= (1f - (0.5f * Time.deltaTime));
        } else if(Vector3.Distance(rigidbody.position, destination) < 1f) {
            rigidbody.velocity = Vector3.zero;
            finished = true;
        } else if (!finished)
        {
            start();
        }
    }

    public override bool isFinished()
    {
        return finished;
    }

    

    public override Vector3 getDestination() { return destination; }
}