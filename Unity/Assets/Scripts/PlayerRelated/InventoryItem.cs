using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    private int itemId;
    [SerializeField]
    private InventoryItemType type;
    [SerializeField]
    private Sprite picture;

    public InventoryItem(int idO, InventoryItemType typeO, Sprite pictureO)
    {
        this.itemId = idO;
        this.type = typeO;
        this.picture = pictureO;
    }


    public enum InventoryItemType
    {
        Note,
        Key
    }

}
