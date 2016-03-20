using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "PuzzleData", menuName = "MinigameData/PuzzleData", order = 1)]
public class PuzzleData : ScriptableObject
{
    [SerializeField]
    private  Vector2Int puzzleDimensions;
    [SerializeField]
    private  Texture texture;
 

    public Vector2Int PuzzleDimensions
    {
        get { return puzzleDimensions; }
    }

    public Texture Texture
    {
        get { return texture; }
    }
    

    [System.Serializable]
    public class Vector2Int
    {
        public Vector2Int(int x , int y)
        {
            this.x = x;
            this.y = y;
        }


        public int x;
        public int y;


        public int GetNumberOfFiels()
        {
            return x * y;
        }
    }
 
}
