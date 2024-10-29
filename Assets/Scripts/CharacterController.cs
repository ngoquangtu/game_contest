using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển của nhân vật

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Lấy Rigidbody2D để điều khiển vật lý
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Nhận đầu vào từ bàn phím (phím WASD hoặc mũi tên)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật theo hướng đầu vào và tốc độ di chuyển
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void RotateTowardsMouse()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Đặt z về 0 để đảm bảo rằng nó nằm trên mặt phẳng 2D

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
