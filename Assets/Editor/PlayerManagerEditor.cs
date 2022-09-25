using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerManager manager = (PlayerManager)target;

      //  GUILayout.BeginArea(new Rect(3, 3, 3, 3));

        if (GUILayout.Button("Set Players: 2"))
        {
            manager.SetStartPlayerCount(2);
        }

        if (GUILayout.Button("Set Players: 3"))
        {
            manager.SetStartPlayerCount(3);
        }

        if (GUILayout.Button("Set Players: 4"))
        {
            manager.SetStartPlayerCount(4);
        }

      //  GUILayout.Space(5);

        if (GUILayout.Button("Spawn Players"))
        {
            manager.InitializePlayers();
        }

      //  GUILayout.EndArea();
    }
}
