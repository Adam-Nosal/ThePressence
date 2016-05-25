using UnityEngine;
using System.Collections;

public class CipherController : MonoBehaviour, iController {

    public CipherData cipherData;

    private CipherView view;

    void Awake() {
        view = gameObject.GetComponent<CipherView>();
    }

    [ContextMenu("Play")]
    public void Init(){
        view.Init(cipherData);
    }

    public void OnClose(){
        view.Close();
    }
}
