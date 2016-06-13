using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PuzzleGameController : MonoBehaviour , iController
{
    private GameObject mSender;
    void Start()
    {
        mView = this.gameObject.GetComponent<PuzzleGameView>();
    }



    [ContextMenu("Play")]
    public void Init(ScriptableObject setting, GameObject sender = null)
    {
        this.puzzleData = (PuzzleData)setting;
        mSender = sender;

        this.gameObject.SetActive(true);
        mView.Init(puzzleData);
        mView.UpdateView();
    }

	
    public void OnClose()
    {
        this.gameObject.SetActive(false);
        GameController.GetInstance().PauseEnemys(false);
        mSender.SendMessage("Close");
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
