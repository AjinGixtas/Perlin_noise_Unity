using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{

    // Bare-bone implementation of GUI
    public override void OnInspectorGUI() {
        MapGenerator mapGen = (MapGenerator)target;
        if (DrawDefaultInspector()) {
            if (mapGen.autoUpdate) {
                mapGen.GenerateMap();
            }
        }
        if (GUILayout.Button("Generate noise map")) {
            Debug.Log("Clicked!");
            mapGen.GenerateMap();
        }
    }
}
