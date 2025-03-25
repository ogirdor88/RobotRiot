using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public InventoryUI ui;

    [SerializeField]
    SerializedDictionary<string, ItemObject> inventory = new();

    private void OnTriggerEnter(Collider other)
    {
        
    }

    //add an item to an inventory
    public void AddItem(ItemObject item)
    {
        var invetID = Guid.NewGuid().ToString();
        inventory.Add(invetID, item);
        ui.AddUIItem(invetID, item);
    }

    //remove the item from the inventory
    public void DropItem(string invenId)
    {
        var item = inventory.GetValueOrDefault(invenId);
        inventory.Remove(invenId);
        ui.RemoveUIItem(invenId);
    }    
}
