using UnityEngine;
using System.Collections;

public class MinigameActivator : MonoBehaviour {

    GameController _controller;



    void Awake()
    {
        _controller = GameController.GetInstance();
    }





}
