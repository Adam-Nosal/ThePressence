using UnityEngine;
using System.Collections;

public class SlimeBehaviour : MonoBehaviour {

	[HideInInspector]
	public EnemyController script;

	[Header("Pool placement properties")]
	[Range(0.1f, 3.0f)]
	public float spawnTime;
	[Range(0, 6)]
	public float spread;
	[Range(0, 10)]
	public float howFarBack;

	[Header("Pool properties")]
	[Range(0.5f, 20.0f)]
	public float lifeSpan;
	[Range(0, 100)]
	public float slowPower;
	[Range(0, 5)]
	public float slowDuration;

	private float nextSpawnTime;
	private Vector3 baseVector;
	private GameObject bloodPoolPrefab;
	private GameObject bloodPoolToCreate;

	// Use this for initialization
	void Start () {
		// just to be sure
		if (spawnTime < 0.1f) {
			spawnTime = 0.1f;
		}
		script = transform.GetComponent<EnemyController> ();
		bloodPoolPrefab = (GameObject) Resources.Load ("EnemyData/BloodPool", typeof(GameObject));
		baseVector = new Vector3();

		if (!bloodPoolPrefab) {
			Debug.LogError ("There's no prefab to instantiate from!");
			Debug.Break ();
		} 

		script.OnEnemyChasing += UseAbility;
	}

	// spewing blobs all around the slime
	void UseAbility () {
		if (nextSpawnTime < Time.time) {
			// create pool randomly behind the slime
			baseVector.Set (Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f), 0.0f);

			// check if it's not a wall

			bloodPoolToCreate = Instantiate (bloodPoolPrefab, 
				baseVector * spread + script.transform.position - script.movement * howFarBack, Quaternion.identity) as GameObject;
			// place in hierarchy
			bloodPoolToCreate.transform.SetParent (script.transform.parent.FindChild("BloodPoolList"));
			// set order in layer
			bloodPoolToCreate.GetComponent<SpriteRenderer>().sortingOrder = -99;
			// when should next pool be spawned
			nextSpawnTime = Time.time + spawnTime;
		}
	}
}
