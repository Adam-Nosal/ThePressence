using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KeyBoardClick : MonoBehaviour {

    public GameObject spawnParent;
    public GameObject spawnPrefab;
    public int imgNumber;

    public void OnClickBehave() {
        var img = GetComponent<Image>();
        var obj = Instantiate(spawnPrefab);
        obj.transform.SetParent(spawnParent.transform, false);
        obj.GetComponent<Image>().sprite = img.sprite;
        spawnParent.GetComponent<CipherCheck>().AddCodeNumber(imgNumber);
    }
}
