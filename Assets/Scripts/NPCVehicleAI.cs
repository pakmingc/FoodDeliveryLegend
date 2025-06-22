using UnityEngine;

[RequireComponent(typeof(VehicleMovement))]
public class NPCVehicleAI : MonoBehaviour
{
    public Waypoint currentWaypoint;
    public float speed = 5f;

    void FixedUpdate()
    {
        if (currentWaypoint == null) return;

        Vector3 direction = (currentWaypoint.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < 0.5f)
        {
            // 隨機前往下一個相鄰Waypoint
            currentWaypoint = currentWaypoint.neighbors[Random.Range(0, currentWaypoint.neighbors.Count)];
        }
    }
}
