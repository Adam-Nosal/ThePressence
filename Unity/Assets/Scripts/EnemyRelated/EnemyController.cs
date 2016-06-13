using UnityEngine;
using System;						// for events
using System.Collections;
using System.Collections.Generic;	// for lists

/*
	EnemyController3 is responsible for assigning waypoints to enemy's route and patrolling the created path.
	Any tweakable parameters are available in the Inspector tab.
	It uses list of empty GameObjects as waypoints.
*/
public class EnemyController : iPausable {

	public bool cyclic;
	[HideInInspector]
	public int state;
	[HideInInspector]
	public Vector3 movement;
	[Range(0,2)]
	public float easeAmount;
	[Range(0,2)]
	public float speed; 				// enemy's speed
	public float waitTime; 				// wait time at each of waypoints
	public event Action OnEnemyChasing;

	private int fromWaypointIndex; 			// index of waypoint we are moving away from
	private int toWaypointIndex;	
	private bool chasing;					// 0 - patrolling, 1 - chasing or going back to patrol
	private bool revertRoute;
	private float distanceBetweenWaypoints;
	private float nextMoveTime; 			// when to move after pause?
	private float percentBetweenWaypoints; 	// 0..1
	private string objectName;

	private Vector3[] chaseWaypoints; 	// waypoints used in "chasing" state, which are: 
										// waypoint from which enemy runs and waypoint to which it runs
	private List<Vector3> iwlist;		// imported list
    [SerializeField]
    private WaypointList ewlist;	// external list

	//==================================================================//

	// Initialization
	void Awake() {
		//InitializeWaypointList (out ewlist);
        
        if (ewlist.waypointList.Count < 1)
        {
			Debug.LogError ("Enemy needs at least 1 waypoint!");
			Debug.Break ();
		}

		// the only thing needed from waypoints is their position
		iwlist = new List<Vector3> ();
		foreach (GameObject go in ewlist.waypointList) {
			iwlist.Add (go.GetComponent<Transform> ().position);
		}

		chaseWaypoints = new Vector3[2]; // [0] - where monster started from, [1] - player's position
		for (int i = 0; i < 2; i++) {
			chaseWaypoints [i] = Vector3.zero;
		}

		chasing = false;
		revertRoute = false;
	}

	// Update is called once per frame
	public override void EnemyUpdate () {
		movement = CalculateMovement ();

		// direction
		if (movement.x < 0) {
			transform.GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			transform.GetComponent<SpriteRenderer> ().flipX = false;
		}

		transform.Translate (movement);
	}

	//==================================================================//

    //void InitializeWaypointList(out List<GameObject> golist) {
    //    // find the list in hierarchy
    //    objectName = transform.parent.name + "/WaypointList";
    //    GameObject obj = GameObject.Find (objectName);
    //    golist = obj.GetComponent<WaypointList> ().waypointList;
    //}

	#region Movement
	// easing effect while going between waypoints
	// when a=1, easing don't occur
	float Ease(float x) {
		float a = easeAmount + 1; // to make it more intuitive, 0 - no easing, anything else - some easing
		return Mathf.Pow(x,a) / (Mathf.Pow(x,a) + Mathf.Pow(1-x,a)); // (x^a)/(x^a + (1-x)^a)
	}

	Vector3 Patrol() {
		// every time enemy comes back from the chase, it has to be notified about it
		chasing = false;

		// waiting at waypoint
		if (Time.time < nextMoveTime) {
			if (revertRoute) {
				iwlist.Reverse ();
				fromWaypointIndex = 0;
				revertRoute = false;
			}
			return Vector3.zero;
		}

		if(iwlist.Count <= 0)
			Debug.LogError("Imported waypoint's list is empty!");
		#region EasedMovement
		toWaypointIndex = (fromWaypointIndex + 1) % iwlist.Count; // next waypoint to go to
		distanceBetweenWaypoints = Vector3.Distance (iwlist [fromWaypointIndex], iwlist [toWaypointIndex]);

		percentBetweenWaypoints += Time.deltaTime * speed * 5 / distanceBetweenWaypoints; // increases more slowly the further away the points are
		percentBetweenWaypoints = Mathf.Clamp01 (percentBetweenWaypoints); // otherwise easing function can get mad, 
		// i.e. check how the function returned by Ease(x) looks like in wolphram or whatever.
		float easedPercentBetweenWaypoints = Ease (percentBetweenWaypoints);
		#endregion

		// after we reach the waypoint
		if (percentBetweenWaypoints >= 1) {
			percentBetweenWaypoints = 0;
			fromWaypointIndex++;
			fromWaypointIndex %= iwlist.Count; // resets fromWaypointIndex after walking through whole route

			if (!cyclic) {
				if (fromWaypointIndex >= iwlist.Count - 1) {
					revertRoute = true;
				}
			}
			nextMoveTime = Time.time + waitTime;
		}

		// finding a point from "fromWaypoint" to "toWaypoint" based on percentage 
		return Vector3.Lerp (iwlist [fromWaypointIndex], iwlist [toWaypointIndex], easedPercentBetweenWaypoints) - transform.position;
	}

	Vector3 Chase() {
		// 1. Remember starting position
		// 2. Move to the target

		if (!chasing) { // remembering, where we came from
			chaseWaypoints [0] = new Vector3 (transform.position.x, transform.position.y);
			chasing = true;
		}

		return Vector3.MoveTowards (transform.position, chaseWaypoints [1], speed / 4) - transform.position;
	}

	Vector3 ReturnFromChase() {
		// after chasing, go back on patrolling route
		if (transform.position == chaseWaypoints [0]) {
			state = 0;
		}

		return Vector3.MoveTowards (transform.position, chaseWaypoints [0], speed / 5) - transform.position;
	}

	Vector3 CalculateMovement() {
		switch (state) 
		{
		case 0:
			return Patrol();
		case 1:
			return Chase();
		case 2:
			return ReturnFromChase();
		default: // just in case...
			Debug.Log( "Unrecognized state detected!" );
			return Vector3.zero;
		} 
	}
	#endregion

	void OnTriggerEnter2D(Collider2D collider) {
		// If player is spotted - CHASE HIM!
		if (collider.tag == "Player") {
			//Debug.Log ("Player spotted");
			chaseWaypoints [1] = new Vector3 (collider.gameObject.GetComponent<Transform> ().position.x, collider.gameObject.GetComponent<Transform> ().position.y);
			state = 1;
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		// Update the position of player
		if (collider.tag == "Player") {
			//Debug.Log ("Player is being chased!");
			chaseWaypoints [1] = new Vector3 (collider.gameObject.GetComponent<Transform> ().position.x, collider.gameObject.GetComponent<Transform> ().position.y);
			OnEnemyChasing ();
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		// Noone significant around? go back to where you came from
		if (collider.tag == "Player") {
			//Debug.Log ("Player lost from sight");
			chaseWaypoints [1] = new Vector3 (this.gameObject.GetComponent<Transform> ().position.x, this.gameObject.GetComponent<Transform> ().position.y);
			state = 2;
		}
	}

}