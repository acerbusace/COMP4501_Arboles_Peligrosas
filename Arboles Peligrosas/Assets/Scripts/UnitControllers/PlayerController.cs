using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Friendly
{
    Animator anim;

    void Start()
    {
        unitName = "Player";
        unitHealth = 100f;
        speed = 100f;
        gatherSpeed = 1f;
        gatherDistance = 5f;
        rotationSpeed = 15f;
        maxVelocity = 10f;

        sfInfo.info = new Dictionary<string, string>();

        anim = GetComponent<Animator>();
    }

    public override void update()
    {
        base.update();
        float move = GetComponent<Rigidbody>().velocity.magnitude;
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


    public override void updateSFInfo()
    {
        HelperFunctions.addToDict(sfInfo.info, "Unit", unitName);
        HelperFunctions.addToDict(sfInfo.info, "Health", ((int)unitHealth).ToString());
        HelperFunctions.addToDict(sfInfo.info, "Speed", ((int)speed).ToString());
    }
}

