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
    protected float maxVelocity;
    protected float gatherSpeed;
    protected float gatherDistance;
    protected float rotationSpeed;

    public Actor()
    {
        actions = new List<Action>();
    }

    void Update()
    {
        update();
    }

    public bool takeDamage(float damage)
    {
        unitHealth -= damage;
        if (unitHealth < 0)
        {
            unitHealth = 0;
            return true;
        }
        return false;
    }

    public virtual void update()
    {
        updateSFInfo();
        updateQueue();

        if (unitHealth <= 0)
            Destroy(gameObject);
    }

    public void updateQueue()
    {
        if (actions.Count > 0)
        {
            actions[0].update();
            if (actions[0].isFinished()) actions.RemoveAt(0);
        }
    }

    public virtual void queueMove(Vector3 destination)
    {
        actions.Add(new Movement(gameObject, destination, speed, maxVelocity, rotationSpeed));
    }
    public void queueMove(RaycastHit hit)
    {
        queueMove(HelperFunctions.hitToVector(hit));
    }

    public void move(RaycastHit hit)
    {
        clear();
        queueMove(hit);
    }

    public void queueGather(RaycastHit hit)
    {
        Vector3 destination = HelperFunctions.hitToVector(hit);
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

    void OnCollisionEnter(Collision col) {
        handleCollision(col);
    }

    public virtual void handleCollision(Collision col)
    {
        if (!HelperFunctions.containsTag("Ground", col.gameObject.tag))
            clear();
    }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
    public virtual void updateSFInfo() {  }
}

public class Friendly : Actor
{
}

public class Enemy : Actor
{
    protected float seekRange;
    protected float wanderRange;
    protected EnemyState state;

    void Update()
    {
        update();

        if (actions.Count == 0)
        {
            Debug.Log("deciding move");
            decideNextMove();
        }
    }

    public void decideNextMove()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            state = EnemyState.Seeking;
            seek();
        }
        else
        {
            state = EnemyState.Wandering;
            wander();
        }
    }

    public void seek()
    {
        Vector3 target = new Vector3(UnityEngine.Random.Range(-seekRange, seekRange), 0f, UnityEngine.Random.Range(-seekRange, seekRange));
        queueMove(transform.position + target);
    }

    public void wander()
    {
        for (int i = 0; i < UnityEngine.Random.Range(2, 10); ++i)
        //for (int i = 0; i < 3; ++i)
        {
            Debug.Log("it should do this 3 times");
            Vector3 target = new Vector3(UnityEngine.Random.Range(-wanderRange, wanderRange), 0f, UnityEngine.Random.Range(-wanderRange, wanderRange));
            queueMove(transform.position + target);
        }
    }
}

public class Flocker : Enemy
{

    public override void queueMove(Vector3 destination)
    {
        actions.Add(new Movement(gameObject, destination, speed, maxVelocity, rotationSpeed, true));
    }

    public override void handleCollision(Collision col)
    {
    }

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

public class Build : Action
{
    public override bool isFinished()
    {
        return false;
    }

    public override void start()
    {
        
    }

    public override void update()
    {
        
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
