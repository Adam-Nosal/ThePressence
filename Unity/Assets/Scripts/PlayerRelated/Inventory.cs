using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

    [SerializeField]
    GameController _controller;

    private List<InventoryItem> List;


    public static Inventory GetInstance()
    {
        lock (padlock)
        {
            if (instance == null)
            {
                GameObject go;
                go = GameObject.FindGameObjectWithTag(Helpers.TagHelper.InventoryTag);
                if (go == null)
                {
                    go = GameObject.Find(Helpers.TagHelper.InventoryTag);

                }
                instance = go.GetComponent<Inventory>();
            }
            return instance;
        }
    }
    static readonly object padlock = new object();
    static private Inventory instance = null;

    public void Start()
    {
        _controller = GameController.GetInstance();
        List = new List<InventoryItem>();
    }

    public void AddItem()
    {
        InventoryItem key = _controller.GetKeyFromInventory();
        if (key != null)
        {
            Debug.Log("Key " + key.name.ToString());
            List.Add(key);
        }

        InventoryItem note = _controller.GetNoteFromInventory();
        if (note != null)
        {
            Debug.Log("Note " + note.name.ToString());
            List.Add(note);
        }
    }

    public List<InventoryItem> GetNotes()
    {
        if (List.Count != 0)
        {
            List<InventoryItem> result = List.Where(item => item.GetItemType() == InventoryItem.InventoryItemType.Note).ToList<InventoryItem>();
            if (result.Count != 0)
            {
                return result;
            }
        }
        return new List<InventoryItem>();
    }
}
