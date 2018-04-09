using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : Friendly
{
    protected float autoGatherRadius;

	void Start ()
    {
        unitName = "Robot";
        unitHealth = 75f;
        gatherSpeed = 0.5f;
        gatherDistance = 5f;
        autoGatherRadius = 150f;

        sfInfo.info = new Dictionary<string, string>();

        agent = GetComponent<NavMeshAgent>();
    }

    public override void update() {
        base.update();

        GameObject closestResource = null;
        if (actions.Count == 0) {
            Debug.Log("no actions to do...");
            if (gatherStatus != GatherStatus.none) {
                Debug.Log("gathering set to: " + gatherStatus.ToString());
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, autoGatherRadius);

                foreach (Collider c in hitColliders)
                {
                    if (gatherStatus == GatherStatus.tree) {
                        if (HelperFunctions.containsTag("Tree", c.gameObject.tag))
                        {
                            if (closestResource == null || 
                                Vector3.Distance(c.transform.position, transform.position) <
                                Vector3.Distance(closestResource.transform.position, transform.position)) {
                                closestResource = c.gameObject;
                            }
                        }
                    } else if (gatherStatus == GatherStatus.stone) {
                        if (HelperFunctions.containsTag("Stone", c.gameObject.tag))
                        {
                            if (closestResource == null || 
                                Vector3.Distance(c.transform.position, transform.position) <
                                Vector3.Distance(closestResource.transform.position, transform.position)) {
                                closestResource = c.gameObject;
                            }
                        }
                    }
                }
            }

            if (closestResource != null) {
                Debug.Log("gathering new resource: " + closestResource.tag);
                queueGather(closestResource);
            }
        }
    }
}
