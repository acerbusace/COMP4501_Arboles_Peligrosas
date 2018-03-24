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

    public SteeringController()
    {
        path = new List<Vector3>();

    }

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        actor = GetComponent<Actor>();
        
        pathRadius = 5f;
        wanderRadius = 3f;
        wanderAngle = 10f;

        wanderDistance = 1f;
        wanderRadius = 0.5f;
        wanderAngle = 0f;
        wanderAngleChange = 5f;
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
                Debug.Log("getting seek vector: " + getSeekVector(path[0]));
                force += getSeekVector(path[0]);
                //force += getWanderVector();
            }
                
            //rigidBody.AddForce(force * actor.getSpeed(), ForceMode.Acceleration);
            rigidBody.AddForce(force, ForceMode.Acceleration);

            if (rigidBody.velocity.magnitude > actor.getMaxVelocity()) rigidBody.velocity = rigidBody.velocity.normalized * actor.getMaxVelocity();
        } else
        {
            path.RemoveAt(0);
        }
	}

    Vector3 getSeekVector(Vector3 target)
    {
        Vector3 velocityToTarget = (target - transform.position).normalized * actor.getMaxVelocity();
        Vector3 seek = velocityToTarget - rigidBody.velocity;
        return seek;
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

        //Vector3 circleCenter = transform.forward.normalized * 
        //Vector3 randomDisplacement = Quaternion.Euler(0f, Random.Range(-wanderAngle, wanderAngle), 0f) * (transform.forward.normalized * Random.Range(5f, 20f));
        //Vector3 direction = randomDisplacement - circleCenter;

        //return (direction.normalized + previousWander).normalized;
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
