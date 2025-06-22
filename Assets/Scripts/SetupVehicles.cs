using UnityEngine;
using UnityEditor;

public class SetupVehicles : EditorWindow
{
    [MenuItem("Tools/Setup All Vehicles")]
    public static void SetupAllVehicles()
    {
        // 搜尋場景內所有嘅 Vehicles (假設車輛嘅命名包含 "Vehicle")
        GameObject[] allVehicles = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject vehicle in allVehicles)
        {
            if (vehicle.name.Contains("Vehicle"))
            {
                // 加入 Rigidbody
                Rigidbody rb = vehicle.GetComponent<Rigidbody>();
                if (rb == null) rb = vehicle.AddComponent<Rigidbody>();

                // 設定 Rigidbody constraints (只允許Y軸旋轉)
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

                // 加入 Box Collider（如果未有Collider嘅話）
                Collider col = vehicle.GetComponent<Collider>();
                if (col == null)
                {
                    BoxCollider boxCollider = vehicle.AddComponent<BoxCollider>();

                    // 自動設定Collider嘅大小
                    Renderer renderer = vehicle.GetComponentInChildren<Renderer>();
                    if (renderer != null)
                    {
                        boxCollider.size = renderer.bounds.size;
                        boxCollider.center = vehicle.transform.InverseTransformPoint(renderer.bounds.center);
                    }
                }

                // 保存更改
                EditorUtility.SetDirty(vehicle);
            }
        }

        Debug.Log("✅ 已成功為所有車輛加入 Rigidbody 同 Collider！");
    }
}
