using UnityEngine;
using System.Collections;

public class GhostBehaviour : MonoBehaviour {

	[Range(0,5)]
	public float fadingSpeed;

	public float timeFullyRevealed;
	public float timeFullyHidden;

	private bool hiddenEnded;		// ghost is not going to stay completely hidden when this flag is set on true
	private bool revealedEnded;		// ghost is not going to stay completely revealed when this flag is set on true
	private bool intervalSet;		// if "true", flag won't be set until the state is changed
	private float endOfStateTime; 	// end of fully hidden or fully revealed state
	private float fixTimeInterval;	
	private float alpha;

	[HideInInspector]
	public EnemyController script;

	// Initialization
	void Start () {
		script = transform.GetComponent<EnemyController> ();
		fixTimeInterval = 0;
		endOfStateTime = 0;
		hiddenEnded = false;
		revealedEnded = false;
		intervalSet = false;

		script.OnEnemyChasing += FullyReveal;
	}

	void FullyReveal() {
		transform.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
	}

	// Update is called once per frame
	void Update () {
		// if the ghost is chasing after player, continue the hide-reveal routine
		// if there's no script, just ignore the behaviour
		if (script && script.state != 1) {
			if (Time.time < endOfStateTime) {
				// waiting for state to end
				intervalSet = false;
			} else {	
				// if the interval is not set, but we just came out of hidden/revealed state,
				// set it, so that smooth hiding/revealing motion can be sustained
				if (!intervalSet) {
					if (hiddenEnded) {
						fixTimeInterval += timeFullyHidden;
						intervalSet = true;
					}
					if (revealedEnded) {
						fixTimeInterval += timeFullyRevealed;
						intervalSet = true;
					}
				}

				alpha = (Mathf.Sin ((Time.time - fixTimeInterval) * fadingSpeed) + 1) / 2;
				transform.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, alpha);

				// when ghost goes into fully-hidden state, he shouldn't go into it again unless it comes from fully-revealed state
				if (alpha < 0.02f && !hiddenEnded) {
					endOfStateTime = Time.time + timeFullyHidden;
					hiddenEnded = true;
					revealedEnded = false;
					// Debug.Log ("Ghost is hidden");
				}

				// same story as above
				if (alpha > 0.98f && !revealedEnded) {
					endOfStateTime = Time.time + timeFullyRevealed;
					revealedEnded = true;
					hiddenEnded = false;
					// Debug.Log ("Ghost is revealed");
				}
			}
		}
	}
}