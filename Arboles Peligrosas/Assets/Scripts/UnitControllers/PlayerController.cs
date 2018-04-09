using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Friendly
{
    Animator anim;

    private float attackingRadius;
    private float damage;
    private float attackCoolDown;
    private float attackingFov;

    void Start()
    {
        unitName = "Player";
        unitHealth = 200f;
        speed = 100f;
        gatherSpeed = 1f;
        gatherDistance = 7.5f;
        rotationSpeed = 15f;
        maxVelocity = 10f;

        attackingRadius = GetComponent<Collider>().bounds.extents.magnitude + 1f;
        damage = 35f;
        attackCoolDown = 1.5f;
        attackingFov = 45f;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void update()
    {
        base.update();
        float move = GetComponent<NavMeshAgent>().velocity.magnitude;
        bool action;
        anim.SetFloat("speed", move);
        if (actions.Count > 0)
        {
            if (actions[0].GetType() == typeof(Gather))
            {
                action = true;
                anim.SetBool("minning", action);
            }
        } else
        {
            action = false;
            anim.SetBool("minning", action);
        }
        
    }

    public void queueAttack(GameObject attacking)
    {
        actions.Add(new PlayerAttackController(gameObject, attacking, speed, maxVelocity, rotationSpeed, attackingRadius, damage, attackCoolDown, attackingFov));
    }

    public void attack(GameObject attacking)
    {
        clear();
        queueAttack(attacking);
    }


    public override void updateSFInfo()
    {
        base.updateSFInfo();
        HelperFunctions.addToDict(sfInfo.info, "Speed", ((int)speed).ToString());
    }
}

