using System;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class WaypointEditor : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointEditor>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected", MessageType.Warning);
        }    
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButton()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("Add Branch Waypoint"))
            {
                AddBranchWaypoint();
            }

            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }

            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }

            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }

        }
    }

    private void AddBranchWaypoint()
    {
        GameObject waypointObj = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint branchesFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        branchesFrom.branches.Add(waypoint);
        waypoint.transform.position = branchesFrom.transform.position;
        waypoint.transform.forward = branchesFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }

        private void CreateWaypointBefore()
    {
        GameObject waypointObj = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);
        Waypoint newWaypoint = waypointObj.GetComponent<Waypoint>();

        Waypoint selectedWaypoint =Selection.activeGameObject.GetComponent<Waypoint>();
        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.previosWaypoint != null)
        {
            newWaypoint.previosWaypoint = selectedWaypoint.previosWaypoint;
            selectedWaypoint.previosWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.previosWaypoint = selectedWaypoint;
        selectedWaypoint.previosWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    private void CreateWaypointAfter()
    {
        GameObject waypointObj = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);
        Waypoint newWaypoint = waypointObj.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;
        newWaypoint.previosWaypoint = selectedWaypoint;
        newWaypoint.previosWaypoint = selectedWaypoint;

        if(selectedWaypoint.previosWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previosWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = newWaypoint;

        }

        selectedWaypoint.nextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    private void RemoveWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        if( selectedWaypoint.nextWaypoint != null )
        {
            selectedWaypoint.nextWaypoint.previosWaypoint = selectedWaypoint.previosWaypoint;
        }

        if( selectedWaypoint.previosWaypoint != null )
        {
            selectedWaypoint.nextWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previosWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }

    private void CreateWaypoint()
    {
        GameObject waypointObj = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();

        if(waypointRoot.childCount > 1)
        {
            waypoint.previosWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.previosWaypoint.nextWaypoint = waypoint;
            waypoint.transform.position = waypoint.previosWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previosWaypoint.transform.forward;
        }

        Selection.activeObject = waypoint.GameObject();
    }
}
