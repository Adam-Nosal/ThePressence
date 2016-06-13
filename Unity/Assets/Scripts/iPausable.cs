using UnityEngine;
using System.Collections;

public abstract class iPausable : MonoBehaviour {

    public bool isPaused = false;

	// Update is called once per frame
	void Update () {

        if (!isPaused)
        {
            this.EnemyUpdate();
        }

	}

    public void Pause(bool value)
    {
        isPaused = value;
    }
    
    public abstract void EnemyUpdate();
}
