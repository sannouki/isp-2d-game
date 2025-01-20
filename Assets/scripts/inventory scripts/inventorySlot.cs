using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; //for Image 

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] public EquiptableItems storedItem; // Serialize the item stored in the slot
    public EquiptableItems StoredItem => storedItem; // Read-only property for access

    [Header("UI")]
    public Image itemImage; // Image to display the equipable item

    public void OnDrop(PointerEventData eventData)
    {
        // Check if the slot is empty
        if (transform.childCount == 0)
        {
            // If the slot is empty, move the dragged item here
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;

            // Move the item to the slot (you might want to use localPosition to avoid screen space issues)
            dropped.transform.SetParent(transform);
            dropped.transform.localPosition = Vector3.zero; // Set the item to the center of the slot (optional)

            // Update the UI image
            itemImage.sprite = draggableItem.item.image;
        }
        else
        {
            // If the slot is occupied, swap items
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            // Get the current item in the slot
            GameObject current = transform.GetChild(0).gameObject;
            DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

            // Swap positions between the current item and the dropped item
            currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
            currentDraggable.transform.localPosition = Vector3.zero; // Reset position if needed

            // Move the dropped item to the current slot
            draggableItem.parentAfterDrag = transform;
            dropped.transform.SetParent(transform);
            dropped.transform.localPosition = Vector3.zero; // Reset position to center slot (optional)

            // Update the UI image
            itemImage.sprite = draggableItem.item.image;
        }
    }
}
