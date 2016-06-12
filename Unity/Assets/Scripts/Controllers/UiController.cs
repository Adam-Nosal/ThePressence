using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiController : MonoBehaviour {

    public List<GameObject> MobileUI;

    void Awake()
    {
#if !UNITY_ANDROID 
        foreach (GameObject go in MobileUI)
        {
            go.SetActive(false);
        }
#endif

    }
}
