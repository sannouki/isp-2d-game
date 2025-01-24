using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("UI References")]
    public Image image; // Ensure this is assigned in the inspector
    public TextMeshProUGUI countText; // Ensure this is assigned in the inspector
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public EquiptableItems item; // Reference to the item data (EquiptableItems)
    [HideInInspector] public int count = 1; // Default count of the item

    public void Start()
    {
        if (item != null)
        {
            InitializeItem(item);
        }
        else
        {
            Debug.LogWarning("Item is not assigned in the inspector.");
        }
    }

    public void InitializeItem(EquiptableItems newItem)
    {
        if (newItem == null)
        {
            Debug.LogError("InitializeItem called with null item.");
            return;
        }

        item = newItem;
        if (image != null)
        {
            image.sprite = newItem.image;
        }
        else
        {
            Debug.LogError("Image reference is not assigned in the inspector.");
        }
        RefreshCount();
    }

    public void RefreshCount()
    {
        if (countText != null)
        {
            countText.text = count.ToString();
        }
        else
        {
            Debug.LogError("CountText reference is not assigned in the inspector.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the original parent and bring the item to the root for easy dragging
        Debug.Log("state: begin dragging");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Move the item to the root to avoid hierarchy constraints
        transform.SetAsLastSibling(); // Bring it to the front
        if (image != null)
        {
            image.raycastTarget = false; // Disable raycast so it won't block the UI during dragging
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the item to follow the mouse position
        Debug.Log("state: dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restore the original parent and re-enable raycast interaction
        Debug.Log("state: successfully ended");
        transform.SetParent(parentAfterDrag); // Restore the parent after dragging ends
        if (image != null)
        {
            image.raycastTarget = true; // Re-enable raycasting to allow interaction
        }
    }

    // Set this up to assign an EquiptableItems object to the item field.
    public void AssignItem(EquiptableItems equiptableItem)
    {
        if (equiptableItem == null)
        {
            Debug.LogError("AssignItem called with null item.");
            return;
        }

        item = equiptableItem;
        if (image != null)
        {
            image.sprite = equiptableItem.image; // Update the UI sprite to match the item
        }
        else
        {
            Debug.LogError("Image reference is not assigned in the inspector.");
        }
    }
}



