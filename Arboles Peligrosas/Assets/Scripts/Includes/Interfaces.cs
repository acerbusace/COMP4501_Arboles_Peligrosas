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
    protected UI_SelectedFrame sfInfo;

    protected string unitName;
    protected float unitHealth;
    protected float speed;
    protected float maxVelocity;
    protected float gatherSpeed;
    protected float gatherDistance;
    protected float rotationSpeed;

    public Actor() { }

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

        if (unitHealth <= 0)
            Destroy(gameObject);

        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
    public virtual void updateSFInfo() {  }

    public float getMaxVelocity()  {  return maxVelocity; }
    public float getSpeed() { return speed; }
}

public class Friendly : Actor
{
    protected List<Action> actions;
    public Friendly()
    {
        actions = new List<Action>();
    }

    public override void update()
    {
        base.update();

        updateQueue();
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

    void OnCollisionEnter(Collision col)
    {
        handleCollision(col);
    }

    public virtual void handleCollision(Collision col)
    {
        if (!HelperFunctions.containsTag("Ground", col.gameObject.tag))
            clear();
    }

    public Action getAction() { if (actions.Count > 0) return actions[0]; else return null; }
}

public class Enemy : Actor
{
    protected EnemyState state;
}

public class Flocker : Enemy
{
    protected Action currAction;
    protected float seekRange;
    protected float wanderRange;
    protected float neighbourhoodRadius;
    protected bool isLeader;
    protected Flocker leader;

    public override void update()
    {
        base.update();

        leader = findLeader();
        if (leader == null) isLeader = true;
        //else isLeader = false;

        if (currAction != null) currAction.update();

        if (isLeader)
        {
            if (currAction == null || currAction.isFinished())
            {
                //decideNextMove();
            }
        }
        else
        {
            //if (actions.Count == 0)
            //    queueMove(leader.getAction().getDestination());

            //GetComponent<Rigidbody>().velocity += getFlockVector() * speed * Time.deltaTime;

            //if (GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
            //{
            //    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxVelocity;
            //}

            //GetComponent<Rigidbody>().AddForce(getFlockVector() * 10f, ForceMode.Acceleration);
        }

    }

    private Flocker findLeader()
    {
        if (leader != null && Vector3.Distance(leader.transform.position, transform.position) < neighbourhoodRadius)
            return leader;

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, neighbourhoodRadius);

        foreach (Collider c in hitColliders)
        {
            if (HelperFunctions.containsTag("Flocker", c.gameObject.tag))
            {
                Flocker f = c.gameObject.GetComponent<Flocker>();
                if (f.getLeaderStatus() && f.gameObject != gameObject)
                {
                    return f;
                }
            }
        }

        return null;
    }

    public void decideNextMove()
    {
        state = EnemyState.Seeking;
        seek();
    }

    public void seek()
    {
        Vector3 target = new Vector3(UnityEngine.Random.Range(-seekRange, seekRange), 0f, UnityEngine.Random.Range(-seekRange, seekRange));
        currAction = new Movement(gameObject, transform.position + target, speed, maxVelocity, rotationSpeed);
    }

    public void wander()
    {
        for (int i = 0; i < UnityEngine.Random.Range(2, 10); ++i)
        //for (int i = 0; i < 3; ++i)
        {
            Vector3 target = new Vector3(UnityEngine.Random.Range(-wanderRange, wanderRange), 0f, UnityEngine.Random.Range(-wanderRange, wanderRange));
            currAction = new Movement(gameObject, transform.position + target, speed, maxVelocity, rotationSpeed);
        }
    }
    public bool getLeaderStatus() { return isLeader; }

    
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
        Debug.Log("update tree");
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

    public void setRemaining (float r)
    {
        remaining = r;
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
    public abstract Vector3 getDestination();
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

    public override Vector3 getDestination()
    {
        return Vector3.zero;
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
