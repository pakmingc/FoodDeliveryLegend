using UnityEngine;
using UnityEditor;

public class SetupVehiclePhysics : EditorWindow
{
    [MenuItem("Tools/Setup All Vehicle Physics")]
    public static void SetupPhysics()
    {
        var vehicles = GameObject.FindGameObjectsWithTag("Vehicle");
        foreach (var vehicle in vehicles)
        {
            Rigidbody rb = vehicle.GetComponent<Rigidbody>();
            if (!rb) rb = vehicle.AddComponent<Rigidbody>();
            rb.mass = 10;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            BoxCollider col = vehicle.GetComponent<BoxCollider>();
            if (!col) col = vehicle.AddComponent<BoxCollider>();
            col.size = new Vector3(1f, 1f, 2f);
            EditorUtility.SetDirty(vehicle);
        }
        Debug.Log("✅ 所有車輛物理設定完成！");
    }
}
