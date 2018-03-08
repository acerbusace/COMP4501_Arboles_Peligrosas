using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Selectable
{
    UI_SelectedFrame getSFInfo();
}

// change name to something more appropriate
public interface Moveable
{
    void move(RaycastHit ray);
    void queueMove(RaycastHit ray);
    void gather(RaycastHit hit);
    void queueGather(RaycastHit hit);
}

public enum Status
{
    PENDING,
    RUNNING,
    FINISHED
}

public interface Action
{
    void start();
    void stop();
    Status getStatus();
}

public class Actor : MonoBehaviour, Selectable, Moveable
{
    protected Action currentAction;
    protected UI_SelectedFrame sfInfo;
    protected List<Action> actions;

    protected string unitName;
    protected float unitHealth;

    public Actor()
    {
        actions = new List<Action>();
    }

    void Update()
    {
        if (actions.Count > 1)
        {
            if (actions[0].getStatus() == Status.FINISHED)
            {
                actions.RemoveAt(0);
                actions[0].start();
            }
        }
    }

    public void queueMove(RaycastHit ray)
    {
        Vector3 destination = hitToVector(ray);
        Movement mov = GetComponent<Movement>();
        mov.addDestination(destination);
        actions.Add(mov);
    }

    public void move(RaycastHit ray)
    {
        clear();
        queueMove(ray);
        GetComponent<Movement>().start();
    }

    public void queueGather(RaycastHit hit)
    {
        Gather gtr = GetComponent<Gather>();
        gtr.addGather(hit.transform.gameObject);

        queueMove(hit);

        actions.Add(gtr);
    }

    public void gather(RaycastHit hit)
    {
        clear();
        queueGather(hit);
        GetComponent<Movement>().start();
    }

    public void clear()
    {
        actions.Clear();
        GetComponent<Movement>().clear();
        //GetComponent<Build>().clear();
        GetComponent<Gather>().clear();
    }

    public Vector3 hitToVector(RaycastHit hit)
    {
        Vector3 destination = new Vector3();
        destination = hit.point;
        destination.y = 0.5f; //terrain.y @ destination.x & destination.z + playerSize.y/2
        return destination;
    }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
}

public class Resource: MonoBehaviour, Selectable
{
    protected float gatherRate;
    protected float remaining; // amount of resource left
    protected UI_SelectedFrame sfInfo;
    
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
        if (remaining <= 0)
        {
            Destroy(gameObject);
        }
    }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
}

public class Tree: Resource
{
    public Tree()
    {
        remaining = 10f;
        gatherRate = 1f;

        sfInfo.info = new Dictionary<string, string>();
        sfInfo.info.Add("Unit", "Tree");
        sfInfo.info.Add("Remaining", ((int)remaining).ToString());
    }
}

public class Stone : Resource
{
    public Stone()
    {
        remaining = 5f;
        gatherRate = 0.25f;

        sfInfo.info = new Dictionary<string, string>();
        sfInfo.info.Add("Unit", "Stone");
        sfInfo.info.Add("Remaining", ((int)remaining).ToString());
    }
}
























public class Interfaces : MonoBehaviour { }
