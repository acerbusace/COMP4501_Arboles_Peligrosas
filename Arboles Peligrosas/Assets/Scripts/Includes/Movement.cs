using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : Action
{
    NavMeshAgent agent;
    private Vector3 destination;
    float speed;
    float maxVelocity;
    float rotationSpeed;

    bool first;
    bool finished;

    public Movement(NavMeshAgent a, Vector3 dest, float s, float mS, float rS)
    {
        agent = a;
        destination = dest;
        speed = s;
        maxVelocity = mS;
        rotationSpeed = rS;

        first = true;
        finished = false;
    }

    public override void start()
    {
        agent.SetDestination(destination);
        first = false;
    }

    public override void update()
    {
        if(first) start();

        if(Vector3.Distance(agent.gameObject.transform.position, destination) < 1f) {
            finished = true;
        }
    }

    public override bool isFinished()
    {
        return finished;
    }

    public override Vector3 getDestination() { return destination; }
}