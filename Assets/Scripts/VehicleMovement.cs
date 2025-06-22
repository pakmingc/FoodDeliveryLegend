using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform currentWaypoint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (currentWaypoint.position - transform.position).normalized;

        // 強制令車只向前行
        Vector3 forwardMovement = transform.forward * speed;
        rb.velocity = new Vector3(forwardMovement.x, rb.velocity.y, forwardMovement.z);

        // 慢慢旋轉朝向Waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f));
    }
}
