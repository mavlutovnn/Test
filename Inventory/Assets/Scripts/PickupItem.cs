using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [Header("Данные предмета")]
    public Item itemData;
    public int amount = 1;

    [Header("Флаг для динамических объектов")]
    public bool spawnedFromInventory = false;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();

        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.mass = Mathf.Clamp(amount * 0.1f, 0.5f, 5f);
        }

        if (col != null)
            col.isTrigger = false;
    }

    IEnumerator Start()
    {
        //Только для динамически спавненных предметов
        if (spawnedFromInventory)
        {
            //поднимаем чуть выше земли, чтобы не проваливался
            transform.position += Vector3.up * 0.2f;

            if (rb != null)
            {
                rb.isKinematic = true;
                yield return new WaitForSeconds(0.05f);
                rb.isKinematic = false;
                rb.WakeUp();
            }
        }

    }

    public void PickUp(Inventory inventory)
    {
        if (inventory == null || itemData == null) return;

        inventory.AddItem(itemData, amount);
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (itemData != null && gameObject.name != itemData.itemName)
            gameObject.name = itemData.itemName;
    }
#endif
}