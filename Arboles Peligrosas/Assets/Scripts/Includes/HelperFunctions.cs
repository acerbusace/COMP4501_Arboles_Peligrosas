using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperFunctions {
    public static bool containsTag(string tagToFind, string tagsToSearchStr) {
        string[] tagsToSearch = tagsToSearchStr.Split(',');

        foreach (string tagtoSearch in tagsToSearch) {
            if (tagToFind == tagtoSearch) return true;
        }

        return false;
    }

    public static Vector3 randomPosition()
    {
        Terrain terrain = GameObject.FindObjectOfType<Terrain>();
        Vector3 size = terrain.terrainData.size;
        Vector3 pos = terrain.transform.position;

        return new Vector3(
            UnityEngine.Random.Range(pos.x - size.x / 2, pos.x + size.x / 2),
            0f,
            UnityEngine.Random.Range(pos.z - size.z / 2, pos.z + size.z / 2)
        );
    }

    public static bool containsTag(List<string> tagsToFind, string tagsToSearchStr) {
        string[] tagsToSearch = tagsToSearchStr.Split(',');

        foreach (string tagToFind in tagsToFind) {
            foreach (string tagtoSearch in tagsToSearch) {
                if (tagToFind == tagtoSearch) return true;
            }
        }

        return false;
    }

    public static void addToDict(Dictionary<string, string> dict, string key, string val)
    {
        string tmp;
        try
        {
            if (dict.TryGetValue(key, out tmp))
                dict[key] = val;
            else
                dict.Add(key, val);
        }
        catch (NullReferenceException e) { }
    }

    public static Vector3 hitToVector(RaycastHit hit, GameObject go = null)
    {
        Vector3 destination = new Vector3();
        destination = hit.point;

        if (go != null)
        {
            if (go.GetComponent<Collider>() != null)
                destination.y = go.GetComponent<Collider>().bounds.size.y / 2; //terrain.y @ destination.x & destination.z + playerSize.y/2
            else if (go.GetComponent<Renderer>() != null)
                destination.y = go.GetComponent<Renderer>().bounds.size.y / 2;
            return destination;
        }

        destination.y = 0f;
        return destination;
    }

    public static bool rotateTowardsVelocity(GameObject gb, float rotationSpeed = 2f, Vector3 direction = new Vector3())
    {
        bool returnValue = false;

        if (direction == Vector3.zero)
        {
            Rigidbody rb = gb.GetComponent<Rigidbody>();
            direction = rb.velocity;
        }

        float step = rotationSpeed * Time.deltaTime;
        direction.y = gb.transform.forward.y;
        Vector3 newDir = Vector3.RotateTowards(gb.transform.forward, direction, step, 0f);

        if (Vector3.Distance(gb.transform.forward, direction.normalized) < step * 2)
            returnValue = true;

        gb.transform.rotation = Quaternion.LookRotation(newDir);

        return returnValue;
    }

    public static bool bakeNavMeshes() {
        GameObject navMesh = GameObject.Find("NavMesh");

        if (navMesh != null) {
            NavMeshSurface[] navMeshSurface = GameObject.Find("NavMesh").GetComponents<NavMeshSurface>();
            foreach (NavMeshSurface nav in navMeshSurface) {
                nav.BuildNavMesh();
            }
        }

        Debug.Log("NavMesh is null and hence cannot bake");
        return false;
    }

    public static Vector3 randomPosition(float xRange, float zRange, float radius, Vector3 pos = new Vector3()) {
        Vector3 position;
        Collider[] hitColliders;

        bool run;
        do {
            run = false;
            position = pos + new Vector3(UnityEngine.Random.Range(-xRange, xRange), 0f, UnityEngine.Random.Range(-zRange, zRange));

            hitColliders = Physics.OverlapSphere(position, radius);
            foreach (Collider hit in hitColliders) {
                if (!containsTag("Ground", hit.gameObject.tag)) {
                    Debug.Log("pos: " + position + " >> " + hit.gameObject.tag);

                    run = true;
                }
            }
        } while (run);

        return position;
    }
}
