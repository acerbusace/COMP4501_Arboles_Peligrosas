using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringController : MonoBehaviour {

    Rigidbody rigidBody;
    Actor actor;
    private List<Vector3> path;
    private float pathRadius; // radius to choose next path
    private float wanderDistance; // distance to wander circle
    private float wanderRadius; // wander circle radius
    private float wanderAngle; // wander angle
    private float wanderAngleChange;

    private float followDistance;

    public SteeringController()
    {
        path = new List<Vector3>();

    }

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        actor = GetComponent<Actor>();
        
        pathRadius = 10f;
        wanderRadius = 3f;
        wanderAngle = 10f;

        wanderDistance = 1f;
        wanderRadius = 0.5f;
        wanderAngle = 0f;
        wanderAngleChange = 10f;
        followDistance = 5f;
    }
	
	// Update is called once per frame
	void Update () {

        if (path.Count == 0) return;

		if (Vector3.Distance(rigidBody.position, path[0]) > pathRadius)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z));

            Vector3 force = new Vector3();
            //force += getObstacleVector() * 1.4f;
            if (GetComponent<Flocker>().getLeaderStatus())
            {
                Debug.Log("only leader: " + GetInstanceID());
                force += getSeekVector(path[0]);
                force += getWanderVector();
            } else {
                Debug.Log("only follower: " + GetInstanceID());
                Flocker leader = GetComponent<Flocker>().getLeader();
                force += followLeader(leader);
            }

            rigidBody.AddForce(force.normalized * actor.getSpeed(), ForceMode.Acceleration);
            //rigidBody.AddForce(force, ForceMode.Acceleration);

            if (rigidBody.velocity.magnitude > actor.getMaxVelocity()) rigidBody.velocity = rigidBody.velocity.normalized * actor.getMaxVelocity();
        } else
        {
            path.RemoveAt(0);
        }
	}

    Vector3 getSeekVector(Vector3 target)
    {
        // results in to rigid of a turn (probably due to drag and gravity)
        //Vector3 velocityToTarget = (target - transform.position).normalized * actor.getMaxVelocity();
        //Vector3 seek = velocityToTarget - rigidBody.velocity.normalized;
        //return seek;

        // provides more for a smoother turning
        Vector3 seek = target - rigidBody.position;
        return seek.normalized;
    }

    Vector3 getObstacleVector()
    {
        Vector3 output = new Vector3();
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward.normalized * 3f, 3f);

        
        foreach (Collider c in hitColliders)
        {
            if (HelperFunctions.containsTag("Tree", c.tag) || HelperFunctions.containsTag("Stone", c.tag))
            {
                Vector3 relativePoint = transform.InverseTransformPoint(c.transform.position);
                if (relativePoint.x < 0.0) //object is to the left
                {
                    output += Quaternion.Euler(0f, 90f, 0f) * transform.forward;
                }
                else //object is to the right
                {
                    output += Quaternion.Euler(0f, -90f, 0f) * transform.forward;
                }
            }
        }

        return output.normalized;
    }

    Vector3 getWanderVector()
    {
        Vector3 circleCenter = transform.forward.normalized * wanderDistance;
        Vector3 displacement = new Vector3(0f, 0f, 1f);
        displacement = displacement.normalized * wanderRadius;

        wanderAngle += Random.Range(-wanderAngleChange, wanderAngleChange);

        displacement = Quaternion.Euler(0f, wanderAngle, 0f) * displacement;

        Vector3 wander = circleCenter + displacement;
        return wander;
    }

    Vector3 followLeader(Flocker leader) {
        Vector3 follow = new Vector3();

        if (leader != null) {
            if (Vector3.Distance(leader.transform.position, transform.position) > followDistance * 2f) {
                Vector3 behind = -leader.transform.forward.normalized * followDistance;
                behind += leader.transform.position;
                follow += behind - transform.position;
            }
        }

        return follow.normalized;
    }

    public void addDestination(Vector3 dest)
    {
        path.Add(dest);
    }

    public void setDestination(Vector3 dest)
    {
        path.Clear();
        addDestination(dest);
    }

    public List<Vector3> getPath()
    {
        return path;
    }
}
