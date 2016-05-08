using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [Header("MiniGamesControllers")]
    [SerializeField]
    PuzzleGameController puzzleGameController;
    [SerializeField]
    CirclesController circlesGameController;


    [Header("Player")]
    PlayerController player;

    
    public static GameController GetInstance()
    {
        lock (padlock)
        {
            if (instance == null)
            {
                GameObject go;
                go = GameObject.FindGameObjectWithTag("Gina");
                if (go == null)
                {
                    go = GameObject.Find("Gina(Singleton)");
                    
                }
                instance = go.GetComponent<GameController>();
            }
            return instance;
        }
    }
    static readonly object padlock = new object();
    static private GameController instance = null;


}
