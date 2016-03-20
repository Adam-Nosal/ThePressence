using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PuzzleGameController : MonoBehaviour , iController
{
    void Awake()
    {
        mView = this.gameObject.GetComponent<PuzzleGameView>();
    }



    [ContextMenu("Play")]
    public void Init()
    {
        mView.Init(puzzleData);
        mView.UpdateView();
    }

    public void OnClose()
    {

    }

    public PuzzleGameView GetView()
    {
        return mView as PuzzleGameView;
    }


    public void PuzzleComplete()
    {
        mView.Close();
        OnClose();
    }
    

    [SerializeField]
    PuzzleGameView mView;

    [SerializeField]
    private PuzzleData puzzleData;
}
