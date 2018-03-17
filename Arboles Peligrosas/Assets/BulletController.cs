using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private float speed;

    private Rigidbody rb;
    private Vector3 startPos;

    private float distTillDestruction;
    private float damage;

	// Use this for initialization
	void Start () {
        speed = 50f;
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        distTillDestruction = 10f;
        damage = 10f;
        rb.velocity = transform.forward * speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(startPos, transform.position) > distTillDestruction)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (HelperFunctions.containsTag("Enemy", collision.gameObject.tag))
        {
            //collision.gameObject.GetComponent<Something>().takeDamage(damage);
        }

        Destroy(gameObject);
    }
}
