using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class PuzzleTile : MonoBehaviour , IPointerDownHandler 
{
	public Vector3 TargetPosition;
    
    
    public bool Active = true;
    
	public bool CorrectLocation = false;
    
	public Vector2 ArrayLocation = new Vector2();
    public Vector2 GridLocation = new Vector2();

    private PuzzleDisplay mPuzzleDisplay;
    private RectTransform mRectTransform;


    void Awake()
	{
		TargetPosition = this.transform.localPosition;
         StartCoroutine(UpdatePosition());
        mRectTransform = this.GetComponent<RectTransform>();
	}

    public void Init(PuzzleDisplay sender, float width, float height)
    {
        mPuzzleDisplay = sender;
        mRectTransform.sizeDelta = new Vector2( width, height);
    }

    public  void LaunchPositionCoroutine(Vector3 newPosition)
	{
		TargetPosition = newPosition;
		StartCoroutine(UpdatePosition());
	}

	public IEnumerator UpdatePosition()
	{
		// whilst we are not at our target position.
		while(TargetPosition != this.transform.localPosition)
		{
			// lerp towards our target.
			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, TargetPosition, 10.0f * Time.deltaTime);
			yield return null;
		}

		// after each move check if we are now in the correct location.
		if(ArrayLocation == GridLocation){CorrectLocation = true;}else{CorrectLocation = false;}

		// if we are not an active tile then hide our renderer and collider.
		if(Active == false)
		{
			this.GetComponent<RawImage>().enabled = false;
			this.GetComponent<Collider2D>().enabled = false;
		}

		yield return null;
	}

	public void ExecuteAdditionalMove()
	{
		// get the puzzle display and return the new target location from this tile. 
		LaunchPositionCoroutine(this.transform.parent.GetComponent<PuzzleDisplay>().GetTargetLocation(this.GetComponent<PuzzleTile>()));
	}
    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mPuzzleDisplay.CanTap())
        LaunchPositionCoroutine(mPuzzleDisplay.GetTargetLocation(this));
    }
}
