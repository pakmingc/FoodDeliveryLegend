using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AutoAssignNeighbors : EditorWindow
{
    [MenuItem("Tools/Auto Assign Neighbors")]
    public static void AssignNeighbors()
    {
        Waypoint[] allWaypoints = GameObject.FindObjectsOfType<Waypoint>();

        float maxDistance = 30f; // 增加距離限制，確保道路嘅 waypoint 可以連上
        float axisTolerance = 1f; // 容許嘅座標偏差，確保係直線或橫線而非斜線連接

        foreach (Waypoint wp in allWaypoints)
        {
            wp.neighbors = new List<Waypoint>();

            foreach (Waypoint otherWp in allWaypoints)
            {
                if (wp == otherWp) continue;

                Vector3 posA = wp.transform.position;
                Vector3 posB = otherWp.transform.position;

                float distance = Vector3.Distance(posA, posB);

                // 判斷 waypoint 是否位於同一直線 (X 軸相近或 Z 軸相近)，但不同時相近
                bool sameX = Mathf.Abs(posA.x - posB.x) <= axisTolerance;
                bool sameZ = Mathf.Abs(posA.z - posB.z) <= axisTolerance;

                // 一定要橫或直（東南西北），唔可以斜線
                if ((sameX || sameZ) && distance < maxDistance)
                {
                    wp.neighbors.Add(otherWp);
                }
            }

            EditorUtility.SetDirty(wp); // 保存修改
        }

        Debug.Log("✅ Neighbors Auto Assigned! (No diagonal connections)");
    }
}
