using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Lerp
{
    private Vector3 start, end;
    private float distance, timeToEnd, currTime;

    public Lerp(Vector3 s, Vector3 e, float speed)
    {
        start = s;
        end = e;
        distance = Vector3.Distance(s, e);
        timeToEnd = distance / speed;
        currTime = 0;
    }

    public Vector3 getPosition()
    {
        currTime += Time.deltaTime;

        if (currTime >= timeToEnd)
            currTime = timeToEnd;

        Vector3 output = Vector3.Lerp(start, end, currTime / timeToEnd);

        if (float.IsNaN(output.x) || float.IsNaN(output.y) || float.IsNaN(output.z)) return start;

        return output;
    }
}

/*public bool isNaN(Vector3 check)
{
    if (float.IsNaN(check.x) || float.IsNaN(check.y) || float.IsNaN(check.z))
        return true;
    return false;
}
*/








public class Movement : MonoBehaviour { }