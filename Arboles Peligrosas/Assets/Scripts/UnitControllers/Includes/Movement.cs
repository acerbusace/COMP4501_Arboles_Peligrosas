using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Action
{

    static float neighbourhoodRadius = 300f;
    Vector3 destination;
    Rigidbody rigidbody;
    float speed;
    float maxVelocity;
    bool flock;
    float rotationSpeed;

    //Flock flock;
    //Seek seek;
    //Wander wander;

    bool first;
    bool finished;

    public Movement(GameObject gb, Vector3 dest, float s, float mS, float rS, bool f = false)
    {
        rigidbody = gb.GetComponent<Rigidbody>();
        destination = dest;
        speed = s;
        maxVelocity = mS;
        flock = f;
        rotationSpeed = rS;

        first = true;
        finished = false;
    }

    public override void start()
    {
        Vector3 dir = destination - rigidbody.position;
        //rigidbody.velocity = dir.normalized * speed;

        if (first)
        {
            rigidbody.velocity = Vector3.zero;
            first = false;
        }

        if (HelperFunctions.rotateTowardsVelocity(rigidbody.gameObject, rotationSpeed, dir))
        {
            rigidbody.velocity += dir.normalized * speed * Time.deltaTime;
            Debug.Log("done rotating");

            if (flock)
            {
                rigidbody.velocity += getFlockVector(rigidbody) * speed * Time.deltaTime;
            }

            if (rigidbody.velocity.magnitude > maxVelocity)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
            }
        }
    }

    public override void update()
    {
        if (!finished) start();

        if (Vector3.Distance(rigidbody.position, destination) < 0.25f) {
            rigidbody.velocity = Vector3.zero;
            finished = true;
        }
    }

    public override bool isFinished()
    {
        return finished;
    }

    public Vector3 getFlockVector(Rigidbody rigidBody)
    {
        Collider[] hitColliders = Physics.OverlapSphere(rigidBody.transform.position, neighbourhoodRadius);

        int counter = 0;
        Vector3 alignment = new Vector3();
        Vector3 cohesion = new Vector3();
        Vector3 seperation = new Vector3();
        foreach (Collider c in hitColliders)
        {
            if (HelperFunctions.containsTag("Flocker", c.gameObject.tag))
            {
                counter++;

                Rigidbody t = c.gameObject.GetComponent<Rigidbody>();

                alignment += t.velocity;
                cohesion += t.position;
                seperation += (t.position - rigidBody.position);
            }
        }

        alignment /= counter;
        alignment = alignment.normalized;

        cohesion /= counter;
        cohesion = cohesion - rigidBody.position;
        cohesion = cohesion.normalized;
        //cohesion /= 2;

        seperation /= counter;
        seperation = -seperation;
        seperation = seperation.normalized;

        return (alignment + cohesion*1.2f + seperation).normalized;
    }
}