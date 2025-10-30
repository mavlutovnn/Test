using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    //Событие для инвентаря
    public event Action OnInventoryChanged;

    public bool AddItem(Item item, int amount = 1)
    {
        // Найти существующий стак предметов, если предмет стакуемый
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.amount < item.maxStack)
                {
                    int space = item.maxStack - slot.amount;
                    int addAmount = Mathf.Min(space, amount);
                    slot.amount += addAmount;
                    amount -= addAmount;

                    OnInventoryChanged?.Invoke();

                    if (amount <= 0) return true;
                }
            }
        }

        // Найти пустой слот
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.amount = amount;

                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                if (slot.amount > amount)
                {
                    slot.amount -= amount;
                }
                else
                {
                    slot.item = null;
                    slot.amount = 0;
                }

                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }
}