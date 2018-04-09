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
    protected float rotationSpeed;

    void Start()
    {
        unitName = "Player";
        unitHealth = 200f;
        gatherSpeed = 1f;
        gatherDistance = 7.5f;
        rotationSpeed = 15f;

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
        float health = unitHealth;
        bool action;
        anim.SetFloat("speed", move);
        anim.SetFloat("health", health);
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
        actions.Add(new PlayerAttackController(gameObject, attacking, rotationSpeed, attackingRadius, damage, attackCoolDown, attackingFov));
    }

    public void attack(GameObject attacking)
    {
        clear();
        queueAttack(attacking);
    }
}

