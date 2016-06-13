using UnityEngine;
using System.Collections;

public class MinigameActivator : MonoBehaviour {

    GameController _controller;

    public GameController.MiniGames MiniGameToActivate;
    [SerializeField]
    public int RewardItemId = 0;



    void Awake()
    {
        _controller = GameController.GetInstance();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Helpers.TagHelper.PlayerTag)
        {
            Debug.Log("[MiniGameActivator] launching minigame " + MiniGameToActivate.ToString());
            _controller.ActivateMinigame(MiniGameToActivate);
        }


    }



}
