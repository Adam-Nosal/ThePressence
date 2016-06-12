using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(WaypointList))]
public class WaypointListEditor : Editor {

	Editor currentWaypointEditor;
	WaypointList script;

	private List<GameObject> wlist; // just for readability's sake

	// ============================================================================

	public override void OnInspectorGUI() {
		script = target as WaypointList;
		wlist = script.waypointList;

		EditorGUI.BeginChangeCheck ();

		#region Vertical
		GUILayout.BeginVertical();
		
		if (GUILayout.Button ("Check inspector")) {
			script.CheckInspector(wlist);
		}

		EditorGUILayout.Separator();

		#region Horizontal
		GUILayout.BeginHorizontal();

		if (GUILayout.Button ("Add waypoint")) {
			wlist.Add(script.CreateWaypoint(wlist, wlist.Count));
		}

		if (GUILayout.Button ("Remove last waypoint")) {
			script.RemoveWaypoint(wlist, wlist.Count - 1);
		}

		GUILayout.EndHorizontal ();
		#endregion

		EditorGUILayout.Separator();

		for(int i = 0; i < wlist.Count; i++) {
			#region Horizontal
			GUILayout.BeginHorizontal();

			EditorGUILayout.ObjectField ("", wlist[i], typeof(GameObject), true, null);

			if(GUILayout.Button("Delete")) {
				script.RemoveWaypoint(wlist, i);
			}

			GUILayout.EndHorizontal();
			#endregion
		}

		GUILayout.EndVertical ();
		#endregion

		EditorGUI.EndChangeCheck ();
		EditorUtility.SetDirty (target);
		//Debug.Log ("Ready to test");
	}

	void OnSceneGUI() { // fancy numbers on waypoints when WaypointList game object is selected
		script = target as WaypointList;
		wlist = script.waypointList;

		if(!Application.isPlaying) {
			for (int i = 0; i < wlist.Count; i++) {
				if (i < 9) {
					Handles.Label (wlist [i].transform.position - new Vector3 (0.03f, 0f, 0f), (i + 1).ToString ());
				} else {
					Handles.Label (wlist [i].transform.position - new Vector3 (0.1f, 0f, 0f), (i + 1).ToString ());
				}
			}
		}
	}
}
