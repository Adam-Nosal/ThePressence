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


    public enum MiniGames
    {
        puzzleGame,
        circleGame
    };
 

    public static GameController GetInstance()
    {
        lock (padlock)
        {
            if (instance == null)
            {
                GameObject go;
                go = GameObject.FindGameObjectWithTag(Helpers.TagHelper.GameControllerTag);
                if (go == null)
                {
                    go = GameObject.Find(Helpers.TagHelper.GameControllerTag);
                    
                }
                instance = go.GetComponent<GameController>();
            }
            return instance;
        }
    }
    static readonly object padlock = new object();
    static private GameController instance = null;


    public void ActivateMinigame(MiniGames game)
    {
        switch(game){

            case MiniGames.circleGame:

                circlesGameController.Init();
                break;

            case MiniGames.puzzleGame:
                puzzleGameController.Init();
                break;

            default:

                break;
        }


    }







}
