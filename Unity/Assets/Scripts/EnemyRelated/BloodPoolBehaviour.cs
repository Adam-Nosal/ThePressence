using UnityEngine;
using System;
using System.Collections;

public class BloodPoolBehaviour : MonoBehaviour {

	[HideInInspector]
	public SlimeBehaviour script;

	private float poolLifeSpan;
	private GameObject poolCreator;
	private AuraManager auraManager;

	void Start() {
		poolCreator = transform.parent.parent.FindChild ("Slime").gameObject;
		script = poolCreator.GetComponent<SlimeBehaviour> ();

		// so that I won't have heart attacks when I don't see them spawning after opening fresh project...
		if (script.lifeSpan < 0.5f) {
			poolLifeSpan = 0.5f;
		} else {
			poolLifeSpan = script.lifeSpan;
		}
	}

	void Update() {
		Destroy (this.gameObject, poolLifeSpan);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		// if the pool is should spawn on wall, just place it right under the monster
		if (collider.tag == "Border") {
			this.GetComponent<Transform> ().position = poolCreator.GetComponent<Transform> ().position;
		}
	}

	void OnTriggerStay2D (Collider2D collider) {
		// as long as enemy is in the pool, keep the debuff timer up
		if (collider.tag == "Player") {
			auraManager = transform.parent.parent.parent.Find ("Player").FindChild ("AuraManager").GetComponent<AuraManager> ();
			auraManager.slime = poolCreator;
			auraManager.ApplySlow (script.slowPower, script.slowDuration);
			auraManager.IncreaseAggro (script.slowDuration);
		}
	}
}