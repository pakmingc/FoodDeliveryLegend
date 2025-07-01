using UnityEngine;
using UnityEditor;

public class SetupVehiclePhysics : EditorWindow
{
    [MenuItem("Tools/Setup All Vehicle Physics")]
    public static void SetupPhysics()
    {
        var vs = GameObject.FindGameObjectsWithTag("Vehicle");
        foreach (var v in vs)
        {
            var rb = v.GetComponent<Rigidbody>() ?? v.AddComponent<Rigidbody>();
            rb.mass = 10;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            var col = v.GetComponent<BoxCollider>() ?? v.AddComponent<BoxCollider>();
            col.center = Vector3.zero;
            col.size = new Vector3(1, 1, 2);

            EditorUtility.SetDirty(v);
        }
        Debug.Log("All vehicle physics configured.");
    }
}
