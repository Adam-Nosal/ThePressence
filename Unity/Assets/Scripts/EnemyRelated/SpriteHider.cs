using UnityEngine;
using System.Collections;

public class SpriteHider : MonoBehaviour {
	void Start() {
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
