using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : Action
{
    private GameObject resource;
    private GameObject gatheringUnit;
    private float gatherSpeed;
    private float gatherDistance;

    private bool finished;
    private bool startCalled;

    public Gather(GameObject r, GameObject gU, float gS, float gD)
    {
        resource = r;
        gatheringUnit = gU;
        gatherSpeed = gS;
        gatherDistance = gD;
        finished = false;
        startCalled = false;
    }

    public override void start()
    {
        // add loading bar???
    }

    public override void update()
    {
        if (!startCalled)
        {
            start();
            startCalled = true;
        }
        if (finished) return;

        try
        {
            if (Vector3.Distance(resource.transform.position, gatheringUnit.transform.position) > gatherDistance) finished = true;

            ResourceController rc = GameObject.Find("__CONTROL_SCRIPTS__").GetComponent<ResourceController>();
            float amountGathered;
            bool depleted = false;

            if (HelperFunctions.containsTag("Tree", resource.tag))
            {
                Tree tree = resource.GetComponent<Tree>();

                depleted = tree.gather(gatherSpeed * Time.deltaTime, out amountGathered);
                rc.addWood(amountGathered);
            }
            else if (HelperFunctions.containsTag("Stone", resource.tag))
            {
                Stone stone = resource.GetComponent<Stone>();

                depleted = stone.gather(gatherSpeed * Time.deltaTime, out amountGathered);
                rc.addStone(amountGathered);
            }

            if (depleted)
            {
                finished = true;
            }
        }
        catch (Exception e)
        {
            finished = true;
        }
    }

    public override bool isFinished()
    {
        return finished;
    }

    public override Vector3 getDestination()
    {
        return resource.transform.position;
    }

}