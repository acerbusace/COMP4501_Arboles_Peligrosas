using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Gather : MonoBehaviour, Action {
    
    private List<GameObject> resources;
    public float gatherSpeed;
    public float gatherDistance;

    private Status status;


    public Gather()
    {
        resources = new List<GameObject>();
        status = Status.PENDING;
    }
    // Use this for initialization
    public void start () {
        if (resources.Count == 0) return;

        status = Status.RUNNING;
    }

    public void addGather(GameObject resource)
    {
        resources.Add(resource);
    }


    public void stop()
    {
        status = Status.FINISHED;

        if (resources.Count == 0) return;

        resources.RemoveAt(0);
    }
	
	// Update is called once per frame
	void Update () {
        try {
            if (status == Status.RUNNING)
            {
                if (Vector3.Distance(resources[0].transform.position, transform.position) > gatherDistance) stop();

                ResourceController rc = GameObject.Find("__CONTROL_SCRIPTS__").GetComponent<ResourceController>();
                float amountGathered;
                bool depleted = false;

                if (HelperFunctions.containsTag("Tree", resources[0].tag))
                {
                    Tree tree = resources[0].GetComponent<Tree>();

                    depleted = tree.gather(gatherSpeed * Time.deltaTime, out amountGathered);
                    rc.addWood(amountGathered);
                }
                else if (HelperFunctions.containsTag("Stone", resources[0].tag))
                {
                    Stone stone = resources[0].GetComponent<Stone>();

                    depleted = stone.gather(gatherSpeed * Time.deltaTime, out amountGathered);
                    rc.addStone(amountGathered);
                }

                if (depleted)
                {
                    stop();
                }
            }
        } catch (Exception e)
        {
            print(e.ToString());
            stop();
        }
	}

    public void clear() {
        resources.Clear();
        stop();
    }

    public Status getStatus()
    {
        return status;
    }

    public void setStatus(Status s) { status = s; }
}
*/