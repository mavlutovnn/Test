using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Настройки взаимодействия")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    [Header("Ссылки")]
    public Camera playerCamera;
    public Inventory playerInventory;

    [Header("UI курсора")]
    public Image cursorImage;
    public Sprite defaultCursor;
    public Sprite interactCursor;

    private PickupItem currentTarget;

    void Update()
    {
        HandleRaycast();
        HandleInput();
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            PickupItem pickup = hit.collider.GetComponent<PickupItem>();

            if (pickup != null)
            {
                currentTarget = pickup;
                SetCursor(interactCursor);
                return;
            }
        }

        // Если ничего не найдено — сброс курсора
        currentTarget = null;
        SetCursor(defaultCursor);
    }

    void HandleInput()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.PickUp(playerInventory);
            currentTarget = null;
            SetCursor(defaultCursor);
        }
    }

    void SetCursor(Sprite sprite)
    {
        if (cursorImage != null && cursorImage.sprite != sprite)
            cursorImage.sprite = sprite;
    }
}
