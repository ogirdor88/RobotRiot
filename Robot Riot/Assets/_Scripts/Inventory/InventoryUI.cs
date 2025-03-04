using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject uiItemPrefab;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    Transform uiInventParent;

    SerializedDictionary<string, Sprite> inventoryUI = new();

    public void AddUIItem(string inventoryID, ItemObject item)
    { 
        var uiItem = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
        uiItem.transform.SetParent(uiInventParent);
        inventoryUI.Add(inventoryID, item.Icon);
        uiItem.Initialize(inventoryID, item, inventory.DropItem);
    }

    public void RemoveUIItem(string inventoryID) 
    {
        var itemUI = inventoryUI.GetValueOrDefault(inventoryID);
        inventoryUI.Remove(inventoryID);
        Destroy(itemUI);
    }
}
