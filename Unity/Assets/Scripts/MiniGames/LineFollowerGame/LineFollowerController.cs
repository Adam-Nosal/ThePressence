using UnityEngine;
using System.Collections;
using System;
using LineFollower;
using System.Collections.Generic;

public class LineFollowerController : MonoBehaviour, iController {

	//Resourser
    public GameObject line;
    public GameObject resource;
	public GameObject startResource;
	public GameObject endResource;
	public GameObject follower;
	public DifficultyEnum level;


	//Colliders
    private Collider2D resourceCollider;
	private Collider2D startCollider;
	private Collider2D endCollider;

	//Lines
    private LineRenderer lineRenderer;

	//Data
	private InputFactory input;
	private GameState gameState;



    // Use this for initialization
    void Start()
	{
		//Data init
		InputFactory.init ();
		input = InputFactory.GetInputInstance ();
		gameState = new GameState ();

		//Collider init
        resourceCollider = resource.GetComponent<Collider2D>();
		startCollider = startResource.GetComponent<Collider2D>();
		endCollider = endResource.GetComponent<Collider2D>();

		//Renderer init
        lineRenderer = line.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (level == DifficultyEnum.MEDIUM) {
			resource.transform.Rotate (Vector3.forward * Time.deltaTime);
			startResource.transform.Rotate (Vector3.forward * Time.deltaTime);
			endResource.transform.Rotate (Vector3.forward * Time.deltaTime);
		} else if (level == DifficultyEnum.HARD) {
			resource.transform.Rotate (Vector3.forward * Time.deltaTime * 3);
			startResource.transform.Rotate (Vector3.forward * Time.deltaTime * 3);
			endResource.transform.Rotate (Vector3.forward * Time.deltaTime * 3);

			resource.transform.Rotate (Vector3.back * Time.deltaTime * 3);
			startResource.transform.Rotate (Vector3.back * Time.deltaTime * 3);
			endResource.transform.Rotate (Vector3.back * Time.deltaTime * 3);
		}


		if (input.IsInput() == false)
        {
			//Game not started
			if (gameState.isStarted && gameState.isEnded) {
				Debug.Log ("Mission Complete");
				gameState.Reset ();
				OnClose ();
			}
			//Game started but touch released without end point. Loose. Have to restart game
			else if (gameState.isStarted) {
				Debug.Log ("Game Started");
				gameState.Reset ();
			} 	else if (gameState.isEnded) {
				Debug.Log ("Game Started");
				gameState.Reset ();
			}

			follower.SetActive (false);

			return;
        }

		follower.SetActive (true);
		Vector3 mouseToGame = Camera.main.ScreenToWorldPoint(input.GetInput());
		mouseToGame.z = 0;
		follower.transform.position = mouseToGame;

		CheckStartAndEnd ();
		CheckCondition ();
    }

    public void Init()
    {
		
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {

        this.gameObject.SetActive(false);
    }

	public void StartChecking(){

	}

	private bool CheckIsOnLine()
	{
		Vector3 mouseToGame = Camera.main.ScreenToWorldPoint(input.GetInput());
		mouseToGame.z = 0;
		return resourceCollider.OverlapPoint(mouseToGame);
	}

	private bool CheckIsStart()
	{
		Vector3 mouseToGame = Camera.main.ScreenToWorldPoint(input.GetInput());
		mouseToGame.z = 0;
		return startCollider.OverlapPoint(mouseToGame);
	}

	private bool CheckIsEnd()
	{
		Vector3 mouseToGame = Camera.main.ScreenToWorldPoint(input.GetInput());
		mouseToGame.z = 0;
		return endCollider.OverlapPoint(mouseToGame);
	}

	private void CheckCondition(){
		if (CheckIsOnLine())
		{
			//do nothing
		}
		else
		{
			gameState.Reset ();
		}
	}

	private void CheckStartAndEnd()
	{
		if (CheckIsStart ()) {
			gameState.isStarted = true;
		}

		if (CheckIsEnd ()) {
			gameState.isEnded = true;
		}
	}

}
