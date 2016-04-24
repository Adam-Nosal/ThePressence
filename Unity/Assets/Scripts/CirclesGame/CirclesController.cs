using UnityEngine;
using System.Collections;
using System;

public class CirclesController : MonoBehaviour, iController {
    public void Init()
    {
        throw new NotImplementedException();
    }

    public void OnClose()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RotateCircle(RectTransform circle)
    {
        circle.Rotate(new Vector3(0, 0, 90));
    }
}
