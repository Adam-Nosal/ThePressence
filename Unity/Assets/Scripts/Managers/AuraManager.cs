using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	Any aura that can affect player in whole game should be placed here.
	This class serves as a contener for such auras and places buffs/debuffs on player.
*/
public class AuraManager : MonoBehaviour {

	[HideInInspector]
	public float modifiedSpeed;
	[HideInInspector]
	public GameObject slime;

	private bool radiusSet;
	private float baseRadius;
	private float auraTimer;

	// Use this for initialization
	void Start () {
		modifiedSpeed = 1.0f;
		radiusSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > auraTimer) {
			modifiedSpeed = 1.0f;
			if (slime) {
				slime.GetComponent<CircleCollider2D> ().radius = baseRadius;
			}
		}
	}

	public void ApplySlow(float power, float duration) {
		auraTimer = Time.time + duration;
		modifiedSpeed = (100 - power)/100;
	}

	public void IncreaseAggro(float power) {
		if (slime) {
			if (!radiusSet) {
				baseRadius = slime.GetComponent<CircleCollider2D> ().radius;
				radiusSet = true;
			}
			slime.GetComponent<CircleCollider2D> ().radius = baseRadius + power;
		}
		// duration is not needed here as IncreaseAggro and ApplySlow have the same duration
	}
}