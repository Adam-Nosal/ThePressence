using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PuzzleGameView : MonoBehaviour, iView<PuzzleData>
{

    void Awake()
    {
        mPuzzleDisplay = this.gameObject.GetComponent<PuzzleDisplay>();
        mController = this.gameObject.GetComponent<PuzzleGameController>();
    }

    public void Init(PuzzleData puzzleData)
    {
        mPuzzleDisplay.Init(puzzleData);
    }

    public void UpdateView()
    {
        mPuzzleDisplay.CreatePuzzleTiles();
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

