using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackController : Action {
    GameObject controllingGb;
    NavMeshAgent controllingAgent;
    GameObject attackingGb;

    float rotationSpeed;

    float attackingRadius;
    float damage;
    float attackCoolDown;
    float currentAttackCoolDown;
    float attackingFov;

    bool first;
    bool finished;

    public PlayerAttackController(GameObject c, GameObject a, float rS, float aR, float d, float aCD, float aF)
    {
        controllingGb = c;
        controllingAgent = controllingGb.GetComponent<NavMeshAgent>();

        attackingGb = a;
        rotationSpeed = rS;

        // max bounds of player + enemy + offset
        attackingRadius = aR + attackingGb.GetComponent<Collider>().bounds.extents.magnitude;
        damage = d;
        attackCoolDown = aCD;
        currentAttackCoolDown = 0f;
        attackingFov = aF;

        first = true;
        finished = false;
    }

    public override void start()
    {
        Vector3 dir = attackingGb.transform.position - controllingGb.transform.position;

        if (dir.magnitude < attackingRadius) {
            controllingAgent.ResetPath();

            if (Vector3.Angle(controllingGb.transform.forward, dir) < attackingFov) {
                if (currentAttackCoolDown < 0f) {
                    attackingGb.GetComponent<Unit>().takeDamage(damage);
                    currentAttackCoolDown = attackCoolDown;
                }
            } else
                HelperFunctions.rotateTowardsVelocity(controllingGb, rotationSpeed, dir);
        } else
            controllingAgent.SetDestination(attackingGb.transform.position);

        //Debug.Log("current cd: " + currentAttackCoolDown + " >> " + attackingRadius);
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