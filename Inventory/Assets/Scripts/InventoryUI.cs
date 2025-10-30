using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Ссылки")]
    public GameObject inventoryPanel;
    public Transform slotsParent;
    public GameObject slotPrefab;

    [Header("Игровые объекты")]
    public Inventory playerInventory;
    public Transform playerCamera;

    private InventorySlotUI[] uiSlots;

    void Start()
    {
        //Автоопределение камеры
        if (playerCamera == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                playerCamera = mainCam.transform;
            else
                Debug.LogWarning("Player Camera не найдена! Установите поле в InventoryUI.");
        }

        // Создаём слоты
        uiSlots = new InventorySlotUI[playerInventory.slots.Count];

        for (int i = 0; i < uiSlots.Length; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            uiSlots[i] = slotUI;

            DraggableItemUI draggable = slotObj.GetComponent<DraggableItemUI>();
            if (draggable != null)
            {
                draggable.slotUI = slotUI;
                draggable.playerInventory = playerInventory;
                draggable.playerCamera = playerCamera;
                draggable.inventoryPanel = inventoryPanel.GetComponent<RectTransform>();
            }
        }

        inventoryPanel.SetActive(false);

        // Подписка на событие изменения инвентаря
        playerInventory.OnInventoryChanged += RefreshUI;

        RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isOpen = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(isOpen);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            var slotData = playerInventory.slots[i];
            uiSlots[i].SetSlot(slotData.item, slotData.amount);
        }
    }
}
