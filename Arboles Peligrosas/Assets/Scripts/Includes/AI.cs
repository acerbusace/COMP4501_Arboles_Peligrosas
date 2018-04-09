using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    SteeringController steering;
    Terrain terrain;
    GameObject target;
    float visionRange;

	// Use this for initialization
	void Start () {
        steering = GetComponent<SteeringController>();
        terrain = FindObjectOfType<Terrain>();
        target = null;
        visionRange = 30f;
	}
	
	// Update is called once per frame
	void Update () {
        //follow the leader if flocker and not leader
        TigerController tiger = GetComponent<TigerController>();
        if (tiger != null)
        {
            if (!tiger.getLeaderStatus())
            {
                steering.setDestination(tiger.getLeaderDestination());
                return;
            }
        }
        
        //is leader or not a flocker
        if (target == null)
        {
            findTarget();
        }

        if (target != null)
        {
            steering.setDestination(target.transform.position);
        }
        else
        {
            wander();
        }
	}

    void findTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, visionRange);

        foreach (Collider c in hitColliders)
        {
            if (c.gameObject != gameObject)
            {
                if (HelperFunctions.containsTag("Friendly", c.gameObject.tag))
                {
                    target = c.gameObject;
                    return;
                }
            }
        }

        target = null;
    }

    void wander()
    {
        if (!steering.hasPath())
        {
            //for (int i = 0; i < 5; i++)
            //    steering.addDestination(randomPosition());

            Debug.Log("calling set destination");
            steering.setDestination(randomPosition());
        }
    }

    Vector3 randomPosition()
    {
        Vector3 size = terrain.terrainData.size;
        Vector3 pos = terrain.transform.position;

        return new Vector3(
            Random.Range(pos.x - size.x / 2, pos.x + size.x / 2),
            0f,
            Random.Range(pos.z - size.z / 2, pos.z + size.z / 2)
        );
    }

}
