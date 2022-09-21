using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;


[CustomEditor(typeof(PickUpManager))]
public class PickUpManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PickUpManager manager = (PickUpManager)target;

        if (GUILayout.Button("Spawn PickUp"))
        {
            manager.PickUpSpawn();
        }
    }
}
