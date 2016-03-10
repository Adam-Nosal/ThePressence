using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PuzzleGameView : MonoBehaviour, iView
{

    void Awake()
    {
        mPuzzleDisplay = this.gameObject.GetComponent<PuzzleDisplay>();
        mController = this.gameObject.GetComponent<PuzzleGameController>();
    }

    public void Init()
    {

    }

    public void UpdateView()
    {
        mPuzzleDisplay.Init();
        mPuzzleDisplay.JugglePuzzles();
    }
    
    public void Close()
    {
        mPuzzleDisplay.Close();
        mPuzzleDisplay.enabled = false;
    }

    [SerializeField]
    PuzzleDisplay mPuzzleDisplay;
    [SerializeField]
    PuzzleGameController mController;

}

