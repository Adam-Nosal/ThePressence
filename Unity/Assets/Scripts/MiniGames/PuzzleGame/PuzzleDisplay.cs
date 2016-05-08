using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PuzzleDisplay : MonoBehaviour 
{
    [SerializeField]
    private PuzzleGameController mController;

	[SerializeField]
    private Texture PuzzleImage;
    
    [SerializeField]
    private int Height = 3;
    [SerializeField]
    private int Width  = 3;
    
    [SerializeField]
    private Vector3 PuzzleScale = new Vector3(1.0f, 1.0f, 1.0f);
    
    [SerializeField]
    private Vector3 PuzzlePosition = new Vector3(0.0f, 0.0f, 0.0f);
    
    [SerializeField]
    private float SeperationBetweenTiles = 0.5f;
    
    [SerializeField]
    private GameObject Tile;
    
    [SerializeField]
    private Shader PuzzleShader;
    
	private GameObject[,] TileDisplayArray;
	private List<Vector3>  DisplayPositions = new List<Vector3>();
    
	private Vector3 Scale;
	private Vector3 Position;
    
    [SerializeField]
    private bool Complete = false;
    [SerializeField]
    private bool EnableTapping = false;
    

    public void Init(PuzzleData pd)
    {
        PuzzleImage = pd.Texture;
        Width = pd.PuzzleDimensions.x;
        Height = pd.PuzzleDimensions.y;

    }



    public void JugglePuzzles()
    {
        StartCoroutine(JugglePuzzle());
    }
	
    public void Close()
    {
           for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    GameObject.Destroy(TileDisplayArray[i, j]);
            }
        }
    }
    
	void Update () 
	{
		this.transform.localPosition = PuzzlePosition;
        
		this.transform.localScale = PuzzleScale;
	}

	public Vector3 GetTargetLocation(PuzzleTile thisTile)
	{
		// check if we can move this tile and get the position we can move to.
		PuzzleTile MoveTo = CheckIfWeCanMove((int)thisTile.GridLocation.x, (int)thisTile.GridLocation.y, thisTile);

		if(MoveTo != thisTile)
		{
			// get the target position for this new tile.
			Vector3 TargetPos = MoveTo.TargetPosition;
			Vector2 GridLocation = thisTile.GridLocation;
			thisTile.GridLocation = MoveTo.GridLocation;

			// move the empty tile into this tiles current position.
			MoveTo.LaunchPositionCoroutine(thisTile.TargetPosition);
			MoveTo.GridLocation = GridLocation;

			// return the new target position.
			return TargetPos;
		}

		// else return the tiles actual position (no movement).
		return thisTile.TargetPosition;
	}

    public bool CanTap()
    {
        return EnableTapping;
    }

    public void CreatePuzzleTiles()
    {
        EnableTapping = false;
        float TileWidth = Screen.height / Width;
        float TileHeigth = Screen.height / Height;

        // using the width and height variables create an array.
        TileDisplayArray = new GameObject[Width, Height];

        // set the scale and position values for this puzzle.
        Scale = new Vector3(1.0f, 1.0f, 1.0f);
        Tile.transform.localScale = Scale;
        // used to count the number of tiles and assign each tile a correct value.
        int TileValue = 0;

        // spawn the tiles into an array.
        for (int j = Height - 1; j >= 0; j--)
        {
            for (int i = 0; i < Width; i++)
            {
                // calculate the position of this tile all centred around Vector3(0.0f, 0.0f, 0.0f).
                Position = new Vector3(((Scale.x * (i + 0.5f)) - (Scale.x * (Width / 2.0f))) * (10.0f + SeperationBetweenTiles) * 10.0f,
                                      ((Scale.z * (j + 0.5f)) - (Scale.z * (Height / 2.0f))) * (10.0f + SeperationBetweenTiles) * 10.0f,
                                      0.0f);

                // set this location on the display grid.
                DisplayPositions.Add(Position);

                // spawn the object into play.
                TileDisplayArray[i, j] = Instantiate(Tile, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
              
                TileDisplayArray[i, j].GetComponent<PuzzleTile>().Init(this, 100, 100);// TileWidth, TileHeigth);
                TileDisplayArray[i, j].gameObject.transform.parent = this.transform;

                // set and increment the display number counter.
                PuzzleTile thisTile = TileDisplayArray[i, j].GetComponent<PuzzleTile>();
                thisTile.ArrayLocation = new Vector2(i, j);
                thisTile.GridLocation = new Vector2(i, j);
                thisTile.LaunchPositionCoroutine(Position);
                TileValue++;
                RawImage tileRawImage = TileDisplayArray[i, j].GetComponent<RawImage>();
                tileRawImage.texture = PuzzleImage;
                tileRawImage.uvRect = new Rect((1.0f / Width) * (i), (1.0f / Height) * (j), (1.0f / Width), (1.0f / Height));
                TileDisplayArray[i, j].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }


    }

    private PuzzleTile CheckMoveLeft(int Xpos, int Ypos, PuzzleTile thisTile)
	{
		// move left 
		if((Xpos - 1)  >= 0)
		{
			// we can move left, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos - 1, Ypos, thisTile);
		}
		
		return thisTile;
	}
	
	private PuzzleTile CheckMoveRight(int Xpos, int Ypos, PuzzleTile thisTile)
	{
		// move right 
		if((Xpos + 1)  < Width)
		{
			// we can move right, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos + 1, Ypos , thisTile);
		}
		
		return thisTile;
	}
	
	private PuzzleTile CheckMoveDown(int Xpos, int Ypos, PuzzleTile thisTile)
	{
		// move down 
		if((Ypos - 1)  >= 0)
		{
			// we can move down, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos, Ypos  - 1, thisTile);
		}
		
		return thisTile;
	}
	
	private PuzzleTile CheckMoveUp(int Xpos, int Ypos, PuzzleTile thisTile)
	{
		// move up 
		if((Ypos + 1)  < Height)
		{
			// we can move up, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos, Ypos  + 1, thisTile);
		}
		
		return thisTile;
	}
	
	private PuzzleTile CheckIfWeCanMove(int Xpos, int Ypos, PuzzleTile thisTile)
	{
		// check each movement direction
		if(CheckMoveLeft(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveLeft(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveRight(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveRight(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveDown(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveDown(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveUp(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveUp(Xpos, Ypos, thisTile);
		}

		return thisTile;
	}

	private PuzzleTile GetTileAtThisGridLocation(int x, int y, PuzzleTile thisTile)
	{
		for(int j = Height - 1; j >= 0; j--)
		{
			for(int i = 0; i < Width; i++)
			{
				// check if this tile has the correct grid display location.
				if((TileDisplayArray[i,j].GetComponent<PuzzleTile>().GridLocation.x == x)&&
				   (TileDisplayArray[i,j].GetComponent<PuzzleTile>().GridLocation.y == y))
				{
					if(TileDisplayArray[i,j].GetComponent<PuzzleTile>().Active == false)
					{
						// return this tile active property. 
						return TileDisplayArray[i,j].GetComponent<PuzzleTile>();
					}
				}
			}
		}

		return thisTile;
	}

	private IEnumerator JugglePuzzle()
	{
        EnableTapping = false;
		yield return new WaitForSeconds(1.0f);

		// hide a puzzle tile (one is always missing to allow the puzzle movement).
		TileDisplayArray[0,0].GetComponent<PuzzleTile>().Active = false;

		yield return new WaitForSeconds(1.0f);

		for(int k = 0; k < 20; k++)
		{
			// use random to position each puzzle section in the array delete the number once the space is filled.
			for(int j = 0; j < Height; j++)
			{
				for(int i = 0; i < Width; i++)
				{		
					// attempt to execute a move for this tile.
					TileDisplayArray[i,j].GetComponent<PuzzleTile>().ExecuteAdditionalMove();

					yield return new WaitForSeconds(0.05f);
				}
			}
		}

		// continually check for the correct answer.
		StartCoroutine(CheckForComplete());
        EnableTapping = true;
		yield return null;
	}

	public IEnumerator CheckForComplete()
	{
		while(Complete == false)
		{
			// iterate over all the tiles and check if they are in the correct position.
			Complete = true;
			for(int j = Height - 1; j >= 0; j--)
			{
				for(int i = 0; i < Width; i++)
				{
					// check if this tile has the correct grid display location.
					if(TileDisplayArray[i,j].GetComponent<PuzzleTile>().CorrectLocation == false)  
					{
						Complete = false;
					}
				}
			}

			yield return null;
		}
				
		// if we are still complete then all the tiles are correct.
		if(Complete)
		{
            PuzzleComplete();
        }

		yield return null;
	}

    private void PuzzleComplete()
    {
        Debug.Log("Puzzle Complete!");
        mController.PuzzleComplete();
    }

	private Vector2 ConvertIndexToGrid(int index)
	{
		int WidthIndex = index;
		int HeightIndex = 0;

		// take the index value and return the grid array location X,Y.
		for(int i = 0; i < Height; i++)
		{
			if(WidthIndex < Width)
			{
				return new Vector2(WidthIndex, HeightIndex);
			}
			else
			{
				WidthIndex -= Width;
				HeightIndex++;
			}
		}

		return new Vector2(WidthIndex, HeightIndex);
	}

	
}
