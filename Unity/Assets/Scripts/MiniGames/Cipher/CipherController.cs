using UnityEngine;
using System.Collections;

public class CipherController : MonoBehaviour, iController {

    public CipherData cipherData;

    private CipherView view;
    private GameObject mSender;

    [ContextMenu("Play")]
    public void Init(ScriptableObject setting, GameObject sender = null)
    {
        this.cipherData = (CipherData)setting;
        mSender = sender;

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
        GameController.GetInstance().PauseEnemys(false);

        mSender.SendMessage("Close");
    }
}
