using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(VehicleMovement))]
public class DeliveryAgent : MonoBehaviour
{
    public Waypoint currentWaypoint;
    public Waypoint destinationWaypoint;
    public float speed = 5f;
    
    private VehicleMovement vehicleMovement;
    private List<Waypoint> path = new List<Waypoint>();
    private int currentPathIndex = 0;
    private bool goingToDestination = true;

    void Start()
    {
        vehicleMovement = GetComponent<VehicleMovement>();
        SetNewPath(currentWaypoint, destinationWaypoint);
    }

    void FixedUpdate()
    {
        if (path == null || path.Count == 0) return;

        Waypoint targetWaypoint = path[currentPathIndex];
        Vector3 direction = (targetWaypoint.transform.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, speed * Time.deltaTime);

        // Smooth rotation to face waypoint
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0; 
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, targetWaypoint.transform.position) < 0.5f)
        {
            currentWaypoint = targetWaypoint;
            currentPathIndex++;

            if (currentPathIndex >= path.Count)
            {
                if (goingToDestination)
                {
                    Debug.Log($"✅ 已抵達目的地 {destinationWaypoint.name}，現在返回 {currentWaypoint.name}");
                    goingToDestination = false;
                    SetNewPath(currentWaypoint, destinationWaypoint);
                }
                else
                {
                    Debug.Log($"✅ 已返回起點 {currentWaypoint.name}，準備下一次送貨。");
                    goingToDestination = true;
                    SetNewPath(currentWaypoint, destinationWaypoint);
                }
            }
        }
    }

    void SetNewPath(Waypoint start, Waypoint end)
    {
        path = FindPath(start, end);
        currentPathIndex = 0;
    }

    List<Waypoint> FindPath(Waypoint start, Waypoint goal)
    {
        SimplePriorityQueue<Waypoint> openSet = new SimplePriorityQueue<Waypoint>();
        openSet.Enqueue(start, 0);
        Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
        Dictionary<Waypoint, float> costSoFar = new Dictionary<Waypoint, float>();

        cameFrom[start] = null;
        costSoFar[start] = 0;

        while (openSet.Count > 0)
        {
            Waypoint current = openSet.Dequeue();

            if (current == goal) break;

            foreach (Waypoint neighbor in current.neighbors)
            {
                float newCost = costSoFar[current] + Vector3.Distance(current.transform.position, neighbor.transform.position);
                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    costSoFar[neighbor] = newCost;
                    float priority = newCost + Vector3.Distance(neighbor.transform.position, goal.transform.position);
                    openSet.Enqueue(neighbor, priority);
                    cameFrom[neighbor] = current;
                }
            }
        }

        return ReconstructPath(cameFrom, start, goal);
    }

    List<Waypoint> ReconstructPath(Dictionary<Waypoint, Waypoint> cameFrom, Waypoint start, Waypoint goal)
    {
        List<Waypoint> path = new List<Waypoint>();
        Waypoint current = goal;
        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}
