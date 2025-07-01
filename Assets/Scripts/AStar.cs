using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    static float H(Waypoint a, Waypoint b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    public static List<Waypoint> FindPath(Waypoint s, Waypoint g)
    {
        var open = new SimplePriorityQueue<Waypoint>();
        var came = new Dictionary<Waypoint, Waypoint>();
        var cost = new Dictionary<Waypoint, float>();

        open.Enqueue(s, 0);
        cost[s] = 0;

        while (open.Count > 0)
        {
            var cur = open.Dequeue();
            if (cur == g) break;

            foreach (var nxt in cur.allowedNextWaypoints)
            {
                float newCost = cost[cur] + H(cur, nxt);
                if (!cost.ContainsKey(nxt) || newCost < cost[nxt])
                {
                    cost[nxt] = newCost;
                    float prio = newCost + H(nxt, g);
                    open.Enqueue(nxt, prio);
                    came[nxt] = cur;
                }
            }
        }

        var path = new List<Waypoint>();
        if (!came.ContainsKey(g)) return path;
        for (var x = g; x != s; x = came[x]) path.Add(x);
        path.Add(s);
        path.Reverse();
        return path;
    }
}
