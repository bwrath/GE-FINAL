using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360f, fov.Radius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.Angle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.Angle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.Radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.Radius);

        foreach (var target in fov.visibleTargets)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, target.position);
        }
    }

    
}
