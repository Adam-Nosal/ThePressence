using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using SmartLocalization;

public class NoteHandler : MonoBehaviour {

    [SerializeField]
    Inventory _inventory;

    [SerializeField]
    private int currentNote = 0;
    [SerializeField]
    private List<InventoryItem> List;
    private int MaxNotes = 5;

    public Text noteText;
    public Button nextButton;
    public Button prevButton;
    

	// Use this for initialization
	void Awake () {
        _inventory = Inventory.GetInstance();
        List = new List<InventoryItem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnEnable()
    {
        Time.timeScale = 0.0f;
        List = _inventory.GetNotes();
        //Debug.Log("Number of Notes: " + _inventory.GetNotes().Count.ToString());
        UpdateButtons();
        ShowNote();

    }

    public void ShowNote()
    {
        if (List.Count != 0)
        {
            Debug.Log("Number of notes: " + List[currentNote].name);
            noteText.text = LanguageManager.Instance.GetTextValue(List[currentNote].name);
        }
    }

    public void CloseHandler()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void UpdateButtons()
    {
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        if (currentNote >= MaxNotes || currentNote >= List.Count - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
        if (currentNote <= 0 || List.Count == 0)
        {
            prevButton.gameObject.SetActive(false);
        }

    }

    public void NextNote()
    {
        currentNote += 1;
        if (currentNote >= MaxNotes)
        {
            currentNote = MaxNotes;
        }
        UpdateButtons();
        ShowNote();
    }

    public void PreviousNote()
    {
        currentNote -= 1;
        if (currentNote <= 0)
        {
            currentNote = 0;
        }
        UpdateButtons();
        ShowNote();
    }
}
