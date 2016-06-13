using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class SpawnKeyboard : MonoBehaviour {

    public GameObject spawnParent;
    public GameObject spawnPrefab;
    public GameObject cipherWindow;

    private int[] myArray;

    public void GenerateKeyboard (int nrOfImages) {

        myArray = Enumerable.Range(0, nrOfImages).ToArray();
        System.Random rnd = new System.Random();
        myArray = myArray.OrderBy(x => rnd.Next()).ToArray();

        foreach (var val in myArray) {
            var obj = Instantiate(spawnPrefab);
            obj.transform.SetParent(spawnParent.transform, false);
            var keyBoardClick = obj.GetComponent<KeyBoardClick>();
            keyBoardClick.spawnParent = cipherWindow;
            keyBoardClick.imgNumber = val;

            var face = obj.GetComponentInChildren<KeyboardFace>();
            face.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Cipher/Mark" + val);
        }

        this.gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(80, 80);
    }
}
