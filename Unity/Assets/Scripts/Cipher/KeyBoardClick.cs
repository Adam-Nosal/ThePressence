using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KeyBoardClick : MonoBehaviour {

    public GameObject spawnParent;
    public GameObject spawnPrefab;
    public int imgNumber;

    public void OnClickBehave() {
        var img = GetComponentInChildren<KeyboardFace>();
        var obj = Instantiate(spawnPrefab);
        obj.transform.SetParent(spawnParent.transform, false);
        obj.GetComponent<Image>().sprite = img.GetComponent<Image>().sprite;
        spawnParent.GetComponent<CipherCheck>().AddCodeNumber(imgNumber);
    }
}
