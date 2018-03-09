using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Action
{

    Vector3 destination;
    Rigidbody rigidbody;
    float speed;

    bool finished;

    public Movement(GameObject gb, Vector3 dest, float s)
    {
        rigidbody = gb.GetComponent<Rigidbody>();
        destination = dest;
        speed = s;

        finished = false;
    }

    public override void start()
    {
        Vector3 dir = destination - rigidbody.position;
        rigidbody.velocity = dir.normalized * speed;
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
}