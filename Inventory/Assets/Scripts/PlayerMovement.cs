using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("��������� ��������")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    [Header("��������� ������")]
    public Transform cameraTransform;
    public float maxLookAngle = 80f;

    private Rigidbody rb;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // ����������� �������� ������������ ������
        Vector3 moveDir = transform.forward * z + transform.right * x;
        moveDir.Normalize();

        // ����������� ����� Rigidbody
        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange.y = 0f; // �� ������� ������������ ��������
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // �������� ������ �� ���������
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �������� ������ �� �����������
        transform.Rotate(Vector3.up * mouseX);
    }
}
