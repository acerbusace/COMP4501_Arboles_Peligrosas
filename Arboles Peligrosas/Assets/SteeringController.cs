using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringController : MonoBehaviour {

    Rigidbody rigidBody;
    Actor actor;
    private List<Vector3> path;
    private float pathRadius; // radius to choose next path

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        actor = GetComponent<Actor>();

        path = new List<Vector3>();
        pathRadius = 5f;

	}
	
	// Update is called once per frame
	void Update () {

        if (path.Count == 0) return;

		if (Vector3.Distance(rigidBody.position, path[0]) > pathRadius)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z));

            rigidBody.AddForce(getSeekVector(path[0]) * actor.getSpeed(), ForceMode.Acceleration);

            if (rigidBody.velocity.magnitude > actor.getMaxVelocity()) rigidBody.velocity = rigidBody.velocity.normalized * actor.getMaxVelocity();
            Debug.Log("vel: " + rigidBody.velocity.magnitude);
        } else
        {
            path.RemoveAt(0);
        }
	}

    Vector3 getSeekVector(Vector3 target)
    {
        Vector3 seek = target - rigidBody.position;
        seek = seek.normalized;

        return seek;
    }

    public void addDestination(Vector3 dest)
    {
        path.Add(dest);
    }
}
