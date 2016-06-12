using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointList : MonoBehaviour {

	public List<GameObject> waypointList = new List<GameObject>();

	private int freshPosIndex; 		// where new waypoints should spawn
	private int emptyObjects;
	private int wronglyNamedObjects;
	private bool freeSpotFound;
	private string objectName;
	private string parentName;

	private Vector3 pos;
	private GameObject newWaypoint;

	// ============================================================================

	public string GetWaypointName(List<GameObject> golist, int index) {
		// waypoint has to be taken from proper parent!
		parentName = transform.parent.name;
		objectName = parentName + "/WaypointList/Waypoint (" + (index + 1) + ")";
		return objectName;
	}

	public GameObject GetWaypoint(List<GameObject> golist, int index) {
		return GameObject.Find (GetWaypointName(golist, index));
	}
		
	public GameObject CreateWaypoint(List<GameObject> golist, int index) {
		// find object to instantiate
		int i = 0;
		while (!golist [i]) {
			i++;
			if (i >= golist.Count) {
				Debug.LogError ("There's no gameObject to create instance from!");
			}
		}
		// create
		newWaypoint = (GameObject) Instantiate(GetWaypoint(golist, i));
		// set parent
		newWaypoint.transform.parent = transform;
		// change name
		newWaypoint.name = "Waypoint (" + (index + 1) + ")";
		// assign position
		newWaypoint.GetComponent<Transform> ().position = FindStartingPosition(golist);
		// assign order in layer
		newWaypoint.GetComponent<SpriteRenderer> ().sortingOrder = 100 + index;

		return newWaypoint;
	}

	public void RemoveWaypoint(List<GameObject> golist, int index) {
		// there always have to be at least 2 waypoints
		if (golist.Count > 2) {
			// destroy last waypoint
			DestroyImmediate (golist [index]);
			// remove it from the list
			golist.RemoveAt (index);
			// rename all others
			if(index < golist.Count) {
				for(int j = index; j < golist.Count; j++) {
					golist[j].name = "Waypoint (" + (j + 1) + ")";
				}
			}
		} else {
			Debug.Log ("Enemy has to have at least 2 waypoints!");
		}
	}

	public void CheckInspector(List<GameObject> golist) {
		// this method should be called whenever user thinks that waypoints may be assigned wrongly in the list
		emptyObjects = 0;
		wronglyNamedObjects = 0;

		// http://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
		for (int i = golist.Count - 1; i >= 0; i--) {
			// if there are empty fields in the list
			if (!golist [i]) {
				// if object doesn't exist
				// this works ONLY if there's still a waypoint to instantiate from
				// it's not fully idiot proof ;)
				if(!GameObject.Find(GetWaypointName(golist, i))) {
					golist.RemoveAt (i);
					golist.Insert (i, CreateWaypoint (golist, i));
					Debug.Log ("Waypoint (" + (i + 1) + ") created and added to list");
				} 
				// if the object exists, but is not assiged to the list
				else {
					golist.RemoveAt (i);
					golist.Insert (i, GetWaypoint (golist, i));
				}
				emptyObjects++;
			} 

			// if waypoint is taken from wrong parent
			// waypoint's grandparent has to be the same as waypointList's parent
			if (golist [i].transform.parent.parent.name != transform.parent.name) {
				Debug.Log ("Waypoint (" + (i + 1) + ") was taken from wrong parent. " +
					"Previous grandparent was: " + golist [i].transform.parent.parent.name +
					" and current one is: " + parentName);
				golist.RemoveAt (i);
				golist.Insert (i, GetWaypoint (golist, i));
				wronglyNamedObjects++;
			}

			// if waypoints are just wrongly named
			if (golist [i].name != "Waypoint (" + (i + 1) + ")") {
				Debug.Log (golist[i].name + " was wrongly named, new name - Waypoint (" + (i + 1) + ").");
				golist [i].name = "Waypoint (" + (i + 1) + ")";
				wronglyNamedObjects++;
			} 
		}

		Debug.Log ( "Inspector checked. " + wronglyNamedObjects + ((wronglyNamedObjects == 1) ? " occurency" : " occurencies") + 
			" repaired and " + emptyObjects + ((emptyObjects == 1) ? " occurency" : " occurencies") + " filled with new data.");
	}

	private Vector3 FindStartingPosition(List<GameObject> golist) {
		freshPosIndex = 0; 
		float deadZone = 0.001f;

		// check every potential position...
		for (int i = 0; i < golist.Count; i++) {
			// presuming there is a free spot and checking if we're wrong
			freeSpotFound = true;
			// for each potential waypoint...
			for (int j = 0; j < golist.Count; j++) {
				pos = golist [j].GetComponent<Transform> ().position - transform.position;
				// j-object is on i-position
				if ((pos.x + deadZone >= 6 + i) && (pos.x - deadZone <= 6 + i) &&
					(pos.y == 0) && (pos.z == 0)) {
					// we have to go deeper!
					freshPosIndex++;
					freeSpotFound = false;
					break;
				} 
			}
			// if there's nothing found, we can stop searching
			if (freeSpotFound) {
				break;
			}
		}

		//Debug.Log ("Index is: " + freshPosIndex);
		pos = transform.position + new Vector3 (6 + freshPosIndex, 0);
		return pos;
	}
}