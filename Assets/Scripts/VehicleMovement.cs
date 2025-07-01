using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    List<Waypoint> path;
    int pathIdx;
    float spd;
    public bool ReachedDestination => path != null && pathIdx >= path.Count;

    public void SetPath(List<Waypoint> pts, float speed)
    {
        path = pts;
        pathIdx = 0;
        spd = speed;
    }

    void Update()
    {
        if (path == null || pathIdx >= path.Count) return;
        Transform tgt = path[pathIdx].transform;
        Vector3 dir = (tgt.position - transform.position).normalized;
        transform.position += dir * spd * Time.deltaTime;
        transform.forward = dir;
        if (Vector3.Distance(transform.position, tgt.position) < .2f) pathIdx++;
    }
}
