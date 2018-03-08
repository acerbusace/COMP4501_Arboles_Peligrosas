using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, Action
{
    private List<Vector3> destinations;
    private Vector3 lastPosition;
    private Lerp movement;
    public float speed;

    Status status;

    public Movement()
    {
        destinations = new List<Vector3>();
    }

    public void start()
    {
        if (destinations.Count == 0) return;

        movement = new Lerp(transform.position, destinations[0], speed);
        status = Status.RUNNING;
    }

    public void stop()
    {
        status = Status.FINISHED;

        if (destinations.Count == 0) return;

        destinations.RemoveAt(0);
    }

    public Status getStatus()
    {
        return status;
    }

    void FixedUpdate()
    {
        if (status == Status.RUNNING)
            move();
    }

    void move()
    {
        lastPosition = transform.position;
        transform.position = movement.getPosition();

        if (lastPosition == transform.position)
        {
            stop();
            return;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (status != Status.RUNNING)
        {
            stop();
            return;
        }

        transform.position = lastPosition;

        //if (col.gameObject.tag != "Ground")
        if (!HelperFunctions.containsTag("Ground", col.gameObject.tag))
            stop();
    }

    public void clear() { destinations.Clear(); stop(); }
    public void addDestination(Vector3 dest) { destinations.Add(dest); }
    public void setSpeed(float s) { speed = s; }
    public float getSpeed() { return speed; }
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

        Vector3 output = Vector3.Lerp(start, end, currTime / timeToEnd);

        if (float.IsNaN(output.x) || float.IsNaN(output.y) || float.IsNaN(output.z)) return start;

        return output;
    }
}

/*public bool isNaN(Vector3 check)
{
    if (float.IsNaN(check.x) || float.IsNaN(check.y) || float.IsNaN(check.z))
        return true;
    return false;
}
*/
