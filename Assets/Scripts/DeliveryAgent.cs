using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryAgent : MonoBehaviour
{
    public Waypoint startWaypoint;
    public List<Waypoint> allDeliveryTargets;
    public float moveSpeed = 8f;
    public TextMeshProUGUI statusText;

    VehicleMovement mover;
    int targetIndex;
    AStarPath currentPath;

    void Start()
    {
        mover = GetComponent<VehicleMovement>();
        targetIndex = 0;
        BeginTrip();
    }

    void BeginTrip()
    {
        if (allDeliveryTargets.Count == 0) return;
        Waypoint dst = allDeliveryTargets[targetIndex];
        currentPath = new AStarPath(startWaypoint, dst);
        mover.SetPath(currentPath.points, moveSpeed);
        if (statusText) statusText.text = $"Deliver → {dst.name}";
    }

    void Update()
    {
        if (currentPath == null) return;
        if (mover.ReachedDestination)
        {
            startWaypoint = allDeliveryTargets[targetIndex];
            targetIndex = (targetIndex + 1) % allDeliveryTargets.Count;
            BeginTrip();
        }
    }
}

class AStarPath
{
    public readonly List<Waypoint> points;
    public AStarPath(Waypoint s, Waypoint g) => points = AStar.FindPath(s, g);
}
