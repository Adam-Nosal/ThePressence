using UnityEngine;
using System.Collections;

public class CipherController : MonoBehaviour, iController {

    public CipherData cipherData;

    private CipherView view;

    [ContextMenu("Play")]
    public void Init(){
        view = gameObject.GetComponent<CipherView>();
        if (view != null) {
            view.Init(cipherData);
        }
        else {
            Debug.LogError("Lack of Cipher View component");
        }
    }

    public void OnClose(){
        view.Close();
    }
}
