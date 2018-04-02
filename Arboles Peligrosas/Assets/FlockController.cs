using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour {

    Actor actor;
    Rigidbody rigidBody;
    private string flockTag;
    private float alignmentRadius;
    private float cohesionRadius;
    private float seperationRadius;

	// Use this for initialization
	void Start () {
        actor = GetComponent<Actor>();
        rigidBody = GetComponent<Rigidbody>();

        flockTag = "Tiger";
        alignmentRadius = 10f;
        cohesionRadius = 10f;
        seperationRadius = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 flock = new Vector3();
        //flock += getAlignmentVector();
        flock += getCohesionVector();
        flock += getSeperationVector() * 1.1f;

        if (flock.magnitude > 0.25f) {
            //rigidBody.AddForce(flock.normalized  * 15f, ForceMode.Acceleration);
            rigidBody.AddForce(flock.normalized  * actor.getSpeed() / 2, ForceMode.Acceleration);
            if (rigidBody.velocity.magnitude > actor.getMaxVelocity()) rigidBody.velocity = rigidBody.velocity.normalized * actor.getMaxVelocity();
        }
    }

    public Vector3 getAlignmentVector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, alignmentRadius);

        int counter = 0;
        Vector3 alignment = new Vector3();
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject != gameObject)
            {
                if (HelperFunctions.containsTag(flockTag, c.gameObject.tag))
                {
                    Rigidbody t = c.gameObject.GetComponent<Rigidbody>();

                    alignment += t.velocity;

                    counter++;
                }
            }
        }

        alignment /= counter;
        return alignment.normalized;
    }

    public Vector3 getCohesionVector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cohesionRadius);
        int counter = 0;
        Vector3 cohesion = new Vector3();
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject != gameObject)
            {
                if (HelperFunctions.containsTag(flockTag, c.gameObject.tag))
                {
                    Rigidbody t = c.gameObject.GetComponent<Rigidbody>();

                    cohesion += t.position;

                    counter++;
                }
            }
        }

        cohesion /= counter;
        cohesion = cohesion - transform.position;
        return cohesion.normalized;
    }

    public Vector3 getSeperationVector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, seperationRadius);

        int counter = 0;
        Vector3 seperation = new Vector3();
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject != gameObject)
            {
                if (HelperFunctions.containsTag(flockTag, c.gameObject.tag))
                {
                    Rigidbody t = c.gameObject.GetComponent<Rigidbody>();

                    seperation += (t.position - rigidBody.position);

                    counter++;
                }
            }
        }

        seperation /= counter;
        seperation = -seperation;
        return seperation.normalized;
    }
}
