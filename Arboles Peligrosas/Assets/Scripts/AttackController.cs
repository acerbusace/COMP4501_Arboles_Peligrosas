﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    private string attackingTag;
    private float attackingRadius;
    private float damage;
    private float attackCoolDown;
    private float currentAttackCoolDown;
    private float attackingFov;

    Animator anim;

	// Use this for initialization
	void Start () {
        attackingTag = "Friendly";
        attackingRadius = GetComponent<Collider>().bounds.extents.magnitude + 1f;
        damage = 25f;
        attackCoolDown = 2f;
        currentAttackCoolDown = 0f;
        attackingFov = 45f;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackingRadius);
        
        foreach (Collider c in hitColliders)
        {
            if (HelperFunctions.containsTag(attackingTag, c.tag))
            {
                if (c.gameObject != gameObject) {
                    Vector3 targetDir = c.gameObject.transform.position - transform.position;
                    //Debug.Log("calling update: " + Vector3.Angle(transform.forward, targetDir));


                    if (Vector3.Angle(transform.forward, targetDir) < attackingFov)
                    {
                        anim.SetBool("attack", true);
                        if (currentAttackCoolDown < 0f)
                        {
                            c.GetComponent<Unit>().takeDamage(damage);
                            currentAttackCoolDown = attackCoolDown;
                        }
                    }
                    else anim.SetBool("attack", false);
                }
            }
        }

        currentAttackCoolDown -= Time.deltaTime;
	}
}
