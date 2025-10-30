using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DraggableItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public InventorySlotUI slotUI;
    [HideInInspector] public Inventory playerInventory;
    [HideInInspector] public Transform playerCamera;
    [HideInInspector] public RectTransform inventoryPanel;

    private RectTransform rectTransform;
    private Canvas parentCanvas;
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool outsideInventory = !RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition, parentCanvas.worldCamera);

        if (outsideInventory)
        {
            DropItem();
        }

        rectTransform.anchoredPosition = originalPosition;
    }

    void DropItem()
    {
        if (slotUI == null || slotUI.currentItem == null) return;

        Item item = slotUI.currentItem;
        int amountToDrop = slotUI.currentAmount;

        if (item.worldPrefab == null)
        {
            Debug.LogWarning($"У предмета {item.itemName} не указан worldPrefab!");
            return;
        }

        // Создаём столько объектов сколько было выброшено
        for (int i = 0; i < amountToDrop; i++)
        {
            Vector3 dropPosition = playerCamera.position + playerCamera.forward * 2f;
            dropPosition += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));

            GameObject droppedObj = Instantiate(item.worldPrefab, dropPosition, Quaternion.identity);

            PickupItem pickup = droppedObj.GetComponent<PickupItem>();
            if (pickup != null)
            {
                pickup.itemData = item;
                pickup.amount = 1;
            }
        }

        playerInventory.RemoveItem(item, amountToDrop);
    }
}