using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Action
{

    Vector3 destination;
    Lerp move;
    GameObject gameObject;
    float speed;

    public Movement(GameObject gb, Vector3 dest, float s)
    {
        gameObject = gb;
        destination = dest;
        speed = s;
    }

    public override void start()
    {
        move = new Lerp(gameObject.transform.position, destination, speed);
    }

    public override void update()
    {
        if (move == null) start();

        gameObject.transform.position = move.getPosition();
    }

    public override bool isFinished()
    {
        if (gameObject.transform.position == destination) return true;
        return false;
    }
}