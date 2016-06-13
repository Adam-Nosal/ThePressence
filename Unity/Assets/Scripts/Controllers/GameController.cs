using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameController : MonoBehaviour {

    [Header("MiniGamesControllers")]
    [SerializeField]
    PuzzleGameController puzzleGameController;
    [SerializeField]
    CirclesController circlesGameController;
    [SerializeField]
    CipherController cipherGameController;

    [Header("InventoryItems")]
    [SerializeField]
    List<InventoryItem> GlobalInventoryItemList;
    int ItemToTakeIndex = 1;
    int MaxItemsToTake = 5;


    [Header("Player")]
    PlayerController player;


    public enum MiniGames
    {
        puzzleGame,
        circleGame,
        cipherGame
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


    public void ActivateMinigame(MiniGames game, ScriptableObject setting,  GameObject sender = null)
    {
        switch(game){

            case MiniGames.circleGame:
                
                circlesGameController.Init(setting,sender);
                break;

            case MiniGames.puzzleGame:
                puzzleGameController.Init(setting, sender);
                break;
            case MiniGames.cipherGame:
                cipherGameController.Init(setting, sender);
                break;

            default:

                break;
        }

        PauseEnemys(true);
        

    }

    public InventoryItem GetKeyFromInventory(int keyId)
    {
        if(GlobalInventoryItemList.Count != 0)
        {
            InventoryItem result = GlobalInventoryItemList.Find(item => item.GetItemId() == keyId);
            //Debug.Log("Key from global " + result.name.ToString());
            if (result != null)
            {
                GlobalInventoryItemList.Remove(result);
                return result;
            }
        }
        return null;
    }

    public InventoryItem GetNoteFromInventory()
    {
        if (GlobalInventoryItemList.Count != 0 && ItemToTakeIndex <= MaxItemsToTake)
        {
            InventoryItem result = GlobalInventoryItemList.Find(item => item.GetItemId() == ItemToTakeIndex + 5);
            if(result != null)
            {
                ItemToTakeIndex += 1;
                GlobalInventoryItemList.Remove(result);
                return result;
            }
        }
        return null;
    }

    public void PauseEnemys(bool value)
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag(Helpers.TagHelper.EnemyTag);

        foreach (GameObject o in list)
        {
            o.SendMessage("Pause", value);
        }
    }

   

}
