using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Unit {

    private Transform head;
    public GameObject bulletPrefab;
    private float bulletSpawnOffsetZ;
    private float bulletSpawnOffsetY;

    private float fireRate;
    private float currFireRate;

    private float detectionRadius;
    private float rotationSpeed;

    private GameObject currTarget;


    // Use this for initialization
    void Start () {
        unitName = "Turret";
        unitHealth = 150f;

        head = transform.Find("Head");
        Collider c = GetComponent<Collider>();
        float z = c.bounds.size.z / 2;
        bulletSpawnOffsetZ = z + 0.75f;
        bulletSpawnOffsetY = c.bounds.size.y;

        fireRate = 1f;
        currFireRate = fireRate;

        detectionRadius = 10f;
        rotationSpeed = 2f;
    }
	
    public override void update() {
        base.update();

        if (currTarget == null || Vector3.Distance(transform.position, currTarget.transform.position) > detectionRadius)
            currTarget = getClosestTarget();

        if (rotateTowardsTarget())
        {
            if (currFireRate < 0)
            {
                shoot();
                currFireRate = fireRate;
            }
        }

        currFireRate -= Time.deltaTime;
    }

    void shoot()
    {
        Vector3 pos = head.position + head.forward * bulletSpawnOffsetZ + new Vector3(0, bulletSpawnOffsetY, 0);
        Instantiate(bulletPrefab, pos, Quaternion.identity).transform.LookAt(currTarget.GetComponent<Collider>().bounds.center);
    }

    GameObject getClosestTarget()
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, detectionRadius);

        GameObject closestTarget = null;
        foreach (Collider hit in hitCollider)
        {
            if (HelperFunctions.containsTag("Enemy", hit.gameObject.tag))
            {
                if (closestTarget == null)
                {
                    closestTarget = hit.gameObject;
                }
                else
                {
                    closestTarget = Vector3.Distance(transform.position, hit.gameObject.transform.position) < Vector3.Distance(transform.position, closestTarget.transform.position) ? hit.gameObject : closestTarget;
                }
            }
        }

        return closestTarget;
    }

    bool rotateTowardsTarget()
    {
        bool returnValue = false;

        if (currTarget != null)
        {
            Vector3 targetDir = currTarget.transform.position - head.position;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(head.forward, targetDir, step, 0f);

            targetDir.y = head.forward.y;
            print("turning distance: " + Vector3.Distance(head.forward, targetDir.normalized));
            if (Vector3.Distance(head.forward, targetDir.normalized) < step * 2)
                returnValue = true;

            head.rotation = Quaternion.LookRotation(newDir);
        }

        return returnValue;
    }
}
