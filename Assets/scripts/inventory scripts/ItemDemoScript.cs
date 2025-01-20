using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDemoScript : MonoBehaviour
{
    public InventoryManagement inventoryManager;
    public EquiptableItems[] itemsToPickup; // Corrected type

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]); // Corrected method name
        if (result == true)
        {
            Debug.Log("Item has been added");
        }
        else
        {
            Debug.Log("Inventory full: item not added");
        }
    }
}