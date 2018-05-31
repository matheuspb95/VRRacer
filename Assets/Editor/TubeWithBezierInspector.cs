using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TubeWithBezier))]
public class TubeWithBezierInspector : Editor {
	public override void OnInspectorGUI () {
		if (GUILayout.Button("Create Circuit")) {
			TubeWithBezier tube = target as TubeWithBezier;
			tube.CreateTube();
		}

		DrawDefaultInspector();
	}
}
