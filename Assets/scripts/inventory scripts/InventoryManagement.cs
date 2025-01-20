using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public InventorySlot[] inventorySlots; // Array to hold inventory slots
    public GameObject inventoryItemPrefab;

    public bool AddItem(EquiptableItems item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false; // Ensure that the method returns false if no empty slot is found
    }

    void SpawnNewItem(EquiptableItems item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem itemInSlot = newItemGo.GetComponent<DraggableItem>();
        if (itemInSlot != null)
        {
            Debug.Log("Initializing item: " + item.name);
            itemInSlot.InitializeItem(item); // Ensure the DraggableItem is properly initialized
        }
        else
        {
            Debug.LogError("DraggableItem component not found on the instantiated prefab.");
        }
    }
}




