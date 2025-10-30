using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("�������� ����������")]
    public string itemID;
    public string itemName;
    [TextArea] public string description;

    [Header("������")]
    public Sprite icon;

    [Header("3D ������ � ����")]
    public GameObject worldPrefab;

    [Header("���������")]
    public bool isStackable = false;
    public int maxStack = 1;

}
