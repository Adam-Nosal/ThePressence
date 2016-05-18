using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CipherCheck : MonoBehaviour {

    public List<int> codePattern;
    List<int> codeToCheck;
    public GameObject codeWindow;
    public Image wrongCode;
    private bool isEqual;

    void Start() {
        isEqual = false;
        CleanUpCode();
    }

    void Update () {
	    CheckCode();
        if (isEqual) {
            Debug.Log(" Win Da Game");
        }
	}

    void CheckCode() {
        if (codePattern.Count == codeToCheck.Count) {
             bool equal = codePattern.SequenceEqual(codeToCheck);
            if (equal) {
                isEqual = equal;
            }
            else {
                CleanUpWindow();
                CleanUpCode();
            }
        }
    }

    void CleanUpWindow() {
        wrongCode.CrossFadeAlpha(255,1,true);
        Invoke("TweenAlpha", 2);
        var childsCollection = codeWindow.GetComponentsInChildren<Image>();
        foreach (var child in childsCollection) {
            Destroy(child.gameObject);
        }
    }

    void TweenAlpha() {
        wrongCode.CrossFadeAlpha(0, 1, true);
    }
    void CleanUpCode() {
        codeToCheck = new List<int>();
    }

    public void AddCodeNumber(int number) {
        codeToCheck.Add(number);
    }
}
