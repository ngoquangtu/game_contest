using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoubleHit : MonoBehaviour
{
    public int hitCount = 0;  // Số lần bị bắn trúng
    public SpriteRenderer spriteRenderer;

    // Danh sách các màu có thể thay đổi sau mỗi lần trúng đạn
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeColor();  // Thiết lập màu ngẫu nhiên ban đầu
    }

    // Hàm gọi khi enemy bị bắn trúng
    public void TakeHit()
    {
        hitCount++;

        if (hitCount >= 2)
        {
            // Enemy bị tiêu diệt sau 2 lần trúng đạn
            Destroy(gameObject);
            GameManager.Instance.AddScore(2);  // Cộng điểm vào game
        }
        else
        {
            // Đổi màu ngẫu nhiên sau mỗi lần trúng đạn
            ChangeColor();
        }
    }

    // Đổi màu sắc ngẫu nhiên cho enemy
    private void ChangeColor()
    {
        int randomIndex = Random.Range(0, colors.Length);
        spriteRenderer.color = colors[randomIndex];
    }

    // Enemy di chuyển về phía người chơi
    void Update()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * Time.deltaTime * 2;  // Tốc độ di chuyển
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            // Kiểm tra nếu đạn trùng màu với enemy
            if (spriteRenderer.color == bullet.GetColor())
            {
                TakeHit(); // Gọi hàm để giảm hit count hoặc phá hủy enemy
                Destroy(collision.gameObject);  // Hủy đạn sau khi va chạm
            }
        }
    }
}

