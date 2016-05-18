using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class SpawnKeyboard : MonoBehaviour {

    public int nrOfImages;
    public GameObject spawnParent;
    public GameObject spawnPrefab;

    private int[] myArray;

    void Start () {
        myArray = Enumerable.Range(0, nrOfImages).ToArray();
        System.Random rnd = new System.Random();
        myArray = myArray.OrderBy(x => rnd.Next()).ToArray();

        foreach (var val in myArray) {
            var obj = Instantiate(spawnPrefab);
            obj.transform.SetParent(spawnParent.transform, false);
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Cipher/Mark" + val);
        }
    }


	void Update () {
	
	}
}
