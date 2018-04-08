using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : Action {
    GameObject controllingGb;
    Rigidbody controllingRb;
    GameObject attackingGb;
    float speed;
    float maxVelocity;
    float rotationSpeed;

    float attackingRadius;
    float damage;
    float attackCoolDown;
    float currentAttackCoolDown;
    float attackingFov;

    bool first;
    bool finished;

    public PlayerAttackController(GameObject c, GameObject a, float s, float mS, float rS, float aR, float d, float aCD, float aF)
    {
        controllingGb = c;
        controllingRb = controllingGb.GetComponent<Rigidbody>();

        attackingGb = a;
        speed = s;
        maxVelocity = mS;
        rotationSpeed = rS;

        attackingRadius = aR;
        damage = d;
        attackCoolDown = aCD;
        currentAttackCoolDown = 0f;
        attackingFov = aF;

        first = true;
        finished = false;
    }

    public override void start()
    {
        Vector3 dir = attackingGb.transform.position - controllingRb.position;

        if (HelperFunctions.rotateTowardsVelocity(controllingRb.gameObject, rotationSpeed, dir))
        {
            if (Vector3.Distance(controllingRb.position, attackingGb.transform.position) < attackingRadius)
            {
                if (Vector3.Angle(controllingGb.transform.forward, attackingGb.transform.position) < attackingFov)
                {
                    if (currentAttackCoolDown < 0f)
                    {
                        attackingGb.GetComponent<Unit>().takeDamage(damage);
                        currentAttackCoolDown = attackCoolDown;
                    }
                }
            } else
            {
                controllingRb.AddForce(dir.normalized * speed, ForceMode.Acceleration);
                if (controllingRb.velocity.magnitude > maxVelocity) controllingRb.velocity = controllingRb.velocity.normalized * maxVelocity;
            }
            
        }
        else {
            controllingRb.velocity = Vector3.zero;
        }

        Debug.Log("current cd: " + currentAttackCoolDown + " >> " + attackingRadius);
        currentAttackCoolDown -= Time.deltaTime;
    }

    public override void update()
    {
        if (!finished) start();

        if (attackingGb.GetComponent<Unit>().isDead())
        {
            finished = true;
        }
    }

    public override bool isFinished()
    {
        return finished;
    }

    public override Vector3 getDestination() { return attackingGb.transform.position; }
}
