using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CipherView : MonoBehaviour, iView<CipherData> {

    private SpawnKeyboard spawnKeyboard;
    private CipherCheck cipherCheck;
    private GameObject gamePrefab;

    public void Init(CipherData so) {
        gamePrefab = Resources.Load<GameObject>("Prefabs/Cipher/Game");
        var game = Instantiate(gamePrefab);
        game.transform.SetParent(this.transform, false);
        spawnKeyboard = GetComponentInChildren<SpawnKeyboard>();
        cipherCheck = GetComponentInChildren<CipherCheck>();
        spawnKeyboard.GenerateKeyboard(so.numberOfImages);
        cipherCheck.SetCodePattern(so.codeNumbers);
    }

    public void UpdateView() {
        throw new System.NotImplementedException();
    }

    public void Close() {
        var tmp = GetComponentInChildren<Image>();
        Destroy(tmp.gameObject);
        
    }
}
