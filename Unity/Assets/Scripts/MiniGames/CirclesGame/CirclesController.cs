using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CirclesController : MonoBehaviour, iController {


    private float rotationAngle;
    public CirclesData difficulty;
    public GameObject[] circles;
    public GameObject winImage;

    private GameObject mSender;
    
    public void Init(ScriptableObject setting, GameObject sender = null)
    {
        this.difficulty = (CirclesData) setting;
        mSender = sender;

        this.gameObject.SetActive(true);
        Debug.Log("Difficulty" + difficulty.difficultyLevel);
        switch (difficulty.difficultyLevel)
        {
            case DifficultyEnum.EASY:
                rotationAngle = 60f;
                RandomizePairs(1);
                break;
            case DifficultyEnum.MEDIUM:
                rotationAngle = 30f;
                RandomizePairs(2);
                break;
            case DifficultyEnum.HARD:
                rotationAngle = 15f;
                RandomizePairs(2);
                break;
        }

        PrepareCircles();

    }

    public void OnClose()
    {

        this.gameObject.SetActive(false);
        GameController.GetInstance().PauseEnemys(false);

        mSender.SendMessage("Close");
    }


    private void RandomizePairs(int pairsNumber)
    {
        Debug.Log(pairsNumber);
        System.Random rnd = new System.Random((int)DateTime.Now.Ticks);
        for (int i = 0; i < pairsNumber; i++)
        {
            bool pairFound = false;
            while (!pairFound)
            {
                int firstCirlce = rnd.Next(0, 4);
                int secondCircle = firstCirlce;
                while(firstCirlce == secondCircle)
                {
                    secondCircle = rnd.Next(0, 4);
                    if (firstCirlce != secondCircle &&
                        circles[secondCircle].GetComponent<Tuple>().isPaired == false)
                    {
                        circles[firstCirlce].GetComponent<Tuple>().tupleCircle = circles[secondCircle];
                        circles[secondCircle].GetComponent<Tuple>().isPaired = true;
                        pairFound = true;
                        Debug.Log("PAIR FOUND" + firstCirlce + " "  + secondCircle);
                    }
                }
            }
        }
    }

    private void PrepareCircles()
    {
        System.Random rnd = new System.Random((int)DateTime.Now.Ticks);
        int rotationsNumber;
        foreach (GameObject circle in circles)
        {
            rotationsNumber = rnd.Next(1, 20);
            for(int i =0; i< rotationsNumber; i++)
            {
                RotateCircle(circle);
            }
        }
    }

   

    public void RotateCircle(GameObject circle)
    {
        circle.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, rotationAngle));
        if (circle.GetComponent<Tuple>().tupleCircle != null)
        {
            for(int i =0; i<3; i++)
            {
                GameObject pairedCircle = circle.GetComponent<Tuple>().tupleCircle.gameObject;
                RotateCircle(pairedCircle);
            }
        }
        CheckWinningConditions();
    }

    private void CheckWinningConditions()
    {
        if(circles[0].GetComponent<RectTransform>().eulerAngles.z== circles[1].GetComponent<RectTransform>().eulerAngles.z
            && circles[0].GetComponent<RectTransform>().eulerAngles.z == circles[2].GetComponent<RectTransform>().eulerAngles.z
            && circles[0].GetComponent<RectTransform>().eulerAngles.z == circles[3].GetComponent<RectTransform>().eulerAngles.z
            && circles[0].GetComponent<RectTransform>().eulerAngles.z == circles[4].GetComponent<RectTransform>().eulerAngles.z)
        {
            OnClose();
        }
    }

    public void HighlightCircle(GameObject chosenCircle)
    {
        Debug.Log("Button was highlighted" + chosenCircle.name );
        chosenCircle.GetComponent<Image>().color = Color.green;
        if (chosenCircle.GetComponent<Tuple>().tupleCircle != null)
        {
            chosenCircle.GetComponent<Tuple>().tupleCircle.GetComponent<Image>().color = Color.green;
        }
    }
    public void UnHighlightCircle(GameObject chosenCircle)
    {
        Debug.Log("Button was highlighted" + chosenCircle.name);
        chosenCircle.GetComponent<Image>().color = Color.white;
        if (chosenCircle.GetComponent<Tuple>().tupleCircle != null)
        {
            chosenCircle.GetComponent<Tuple>().tupleCircle.GetComponent<Image>().color = Color.white;
        }
    }
}
