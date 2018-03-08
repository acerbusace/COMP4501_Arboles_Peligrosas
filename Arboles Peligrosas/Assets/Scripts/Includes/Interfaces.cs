using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Selectable
{
    UI_SelectedFrame getSFInfo();
    void updateSFInfo();
}

public enum Status
{
    PENDING,
    RUNNING,
    FINISHED
}

public abstract class Actor : MonoBehaviour, Selectable
{
    protected Action currentAction;
    protected UI_SelectedFrame sfInfo;
    protected List<Action> actions;

    protected string unitName;
    protected float unitHealth;
    protected float speed;
    protected float gatherSpeed;
    protected float gatherDistance;

    public Actor()
    {
        actions = new List<Action>();
    }

    void Update()
    {
        updateSFInfo();
        if (actions.Count > 0)
        {
            actions[0].update();
            if (actions[0].isFinished()) actions.RemoveAt(0);
        }
    }
    public void queueMove(Vector3 destination)
    {
        actions.Add(new Movement(gameObject, destination, speed));
    }
    public void queueMove(RaycastHit hit)
    {
        queueMove(hitToVector(hit));
    }

    public void move(RaycastHit hit)
    {
        clear();
        queueMove(hit);
    }

    public void queueGather(RaycastHit hit)
    {
        Vector3 destination = hitToVector(hit);
        Vector3 direction = destination - transform.position;
        direction = Vector3.Normalize(direction);
        destination -= direction * 2.5f;

        queueMove(destination);
        actions.Add(new Gather(hit.transform.gameObject, transform.gameObject, gatherSpeed, gatherDistance));
    }

    public void gather(RaycastHit ray)
    {
        clear();
        queueGather(ray);
    }

    public void clear()
    {
        actions.Clear();
    }

    public Vector3 hitToVector(RaycastHit hit)
    {
        Vector3 destination = new Vector3();
        destination = hit.point;
        destination.y = GetComponent<Collider>().bounds.size.y/2; //terrain.y @ destination.x & destination.z + playerSize.y/2
        return destination;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!HelperFunctions.containsTag("Ground", col.gameObject.tag))
            clear();
    }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
    public virtual void updateSFInfo() {  }
}

public class Resource: MonoBehaviour, Selectable
{
    protected float gatherRate;
    protected float remaining; // amount of resource left
    protected UI_SelectedFrame sfInfo;
    protected string unitName;
    
    /*
     * gatherAmount: amount of resource to gather
     * amountGathered: actual amount of resource gathered
     */
    public bool gather(float gatherAmount, out float amountGathered)
    {
        gatherAmount *= gatherRate;
        if (remaining > gatherAmount)
        {
            remaining -= gatherAmount;
            amountGathered = gatherAmount;
            return false;
        } else
        {
            amountGathered = remaining;
            remaining = 0f;
            return true;
        }
    }

    void Update()
    {
        updateSFInfo();
        if (remaining <= 0)
        {
            Destroy(gameObject);
        }
    }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
    public void updateSFInfo()
    {
        HelperFunctions.addToDict(sfInfo.info, "Unit", unitName);
        HelperFunctions.addToDict(sfInfo.info, "Remaining", ((int)remaining).ToString());
    }
}

public class Tree: Resource
{
    public Tree()
    {
        unitName = "Tree";
        remaining = 10f;
        gatherRate = 1f;

        sfInfo.info = new Dictionary<string, string>();
    }
}

public class Stone : Resource
{
    public Stone()
    {
        unitName = "Stone";
        remaining = 5f;
        gatherRate = 0.25f;

        sfInfo.info = new Dictionary<string, string>();
    }
}






public abstract class Action
{

    public abstract void update();
    public abstract bool isFinished();
    public abstract void start();
}

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

    public override void start()   {
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











public class Interfaces : MonoBehaviour { }
