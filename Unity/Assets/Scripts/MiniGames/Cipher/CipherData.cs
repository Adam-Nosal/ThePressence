using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CipherData", menuName = "MinigameData/CipherData", order = 2)]
public class CipherData : ScriptableObject {

    [SerializeField]
    public int numberOfImages;

    [SerializeField]
    public int[] codeNumbers;

}