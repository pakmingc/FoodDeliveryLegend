using UnityEngine;
using UnityEditor;

public class AutoAssignCurrentWaypoint : EditorWindow
{
    public Transform initialWaypoint; // 改成 Transform

    [MenuItem("Tools/Auto Assign Initial Waypoint")]
    public static void ShowWindow()
    {
        GetWindow<AutoAssignCurrentWaypoint>("Auto Assign Initial Waypoint");
    }

    void OnGUI()
    {
        GUILayout.Label("Assign Initial Waypoint to All Vehicles", EditorStyles.boldLabel);
        initialWaypoint = (Transform)EditorGUILayout.ObjectField("Initial Waypoint", initialWaypoint, typeof(Transform), true);

        if (GUILayout.Button("Assign Waypoint"))
        {
            if (initialWaypoint == null)
            {
                Debug.LogError("請指定一個初始Waypoint!");
                return;
            }

            VehicleMovement[] vehicles = FindObjectsOfType<VehicleMovement>();
            foreach (var vehicle in vehicles)
            {
                vehicle.currentWaypoint = initialWaypoint;
                EditorUtility.SetDirty(vehicle);
            }
            Debug.Log($"成功將 {vehicles.Length} 個車輛嘅初始Waypoint設定為 {initialWaypoint.name}");
        }
    }
}
