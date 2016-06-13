using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CirclesData", menuName = "MinigameData/CirclesData", order = 3)]
public class CirclesData : ScriptableObject {

    [SerializeField]
    public DifficultyEnum difficultyLevel;

}