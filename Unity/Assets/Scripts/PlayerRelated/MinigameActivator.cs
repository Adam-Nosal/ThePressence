using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameActivator : MonoBehaviour {

    [SerializeField]
    GameController _controller;
    Inventory _inventory;

    public GameController.MiniGames MiniGameToActivate;
    [SerializeField]
    public int RewardItemId = 0;
    public int NoteItemId = 0;



    void Awake()
    {
        _controller = GameController.GetInstance();
        _inventory = Inventory.GetInstance();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Helpers.TagHelper.PlayerTag)
        {
            Debug.Log("[MiniGameActivator] launching minigame " + MiniGameToActivate.ToString());
            _controller.ActivateMinigame(MiniGameToActivate);
        }

        _inventory.AddItem();

    }

    public void OnDestroy()
    {
        //_inventory.AddItem();
    }



}
