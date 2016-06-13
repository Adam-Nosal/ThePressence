using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	private bool dead;
	private float restartTimer;
	private float restartDelay = 4f;
    [SerializeField]
	private Animator animator;
    [SerializeField]
	private PlayerController player;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private AudioClip GameOverSound;

	void Awake () {
		//animator = GetComponent<Animator> ();
		//player = transform.parent.Find ("Player").GetComponent<PlayerController> ();
		player.OnPlayerDeath += Display;
	}

	void Display() {
		animator.SetTrigger ("GameOver");
        AudioSource.PlayOneShot(GameOverSound);
		dead = true;
	}

	void Update () {
		if (dead) {
			restartTimer += Time.deltaTime;
			if (restartTimer >= restartDelay) {
				dead = false;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}
