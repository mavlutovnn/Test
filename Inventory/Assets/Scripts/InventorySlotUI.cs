using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI элементы")]
    public Image icon;
    public TextMeshProUGUI amountText;

    [HideInInspector]
    public Item currentItem;
    [HideInInspector]
    public int currentAmount;

    public void SetSlot(Item item, int amount)
    {
        currentItem = item;
        currentAmount = amount;

        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            amountText.text = item.isStackable && amount > 1 ? amount.ToString() : "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        currentAmount = 0;
        icon.enabled = false;
        amountText.text = "";
    }
}
