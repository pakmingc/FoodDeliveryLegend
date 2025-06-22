using UnityEditor;
using UnityEngine;

public class BatchRenameWaypoints : EditorWindow
{
    private string baseName = "Waypoint";
    private int startIndex = 0;

    [MenuItem("Tools/Batch Rename Waypoints")]
    public static void ShowWindow()
    {
        GetWindow<BatchRenameWaypoints>("Batch Rename Waypoints");
    }

    void OnGUI()
    {
        GUILayout.Label("Rename Selected Objects", EditorStyles.boldLabel);

        baseName = EditorGUILayout.TextField("Base Name", baseName);
        startIndex = EditorGUILayout.IntField("Start Index", startIndex);

        if (GUILayout.Button("Rename Selected"))
        {
            RenameSelected();
        }
    }

    void RenameSelected()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected for renaming.");
            return;
        }

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            selectedObjects[i].name = baseName + "_" + (startIndex + i).ToString("D3");
        }
    }
}
