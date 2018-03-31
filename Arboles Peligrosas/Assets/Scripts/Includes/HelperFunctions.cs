using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions {
    public static bool containsTag(string tagToFind, string tagsToSearchStr) {
        string[] tagsToSearch = tagsToSearchStr.Split(',');

        foreach (string tagtoSearch in tagsToSearch) {
            if (tagToFind == tagtoSearch) return true;
        }

        return false;
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
}
