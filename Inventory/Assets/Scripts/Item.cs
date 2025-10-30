using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Основная информация")]
    public string itemID;
    public string itemName;
    [TextArea] public string description;

    [Header("Визуал")]
    public Sprite icon;

    [Header("3D объект в мире")]
    public GameObject worldPrefab;

    [Header("Настройки")]
    public bool isStackable = false;
    public int maxStack = 1;

}
