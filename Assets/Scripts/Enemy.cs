using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    public string enemyColor; // Xanh, Đỏ hoặc Vàng
    public int hitCount = 0;  // Số lần bị bắn trúng
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Thiết lập màu sắc cho enemy
        SetColor();
    }

    void Update()
    {
        if (player != null)
        {
            // Di chuyển về phía người chơi
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void SetColor()
    {
        // Gán màu sắc ngẫu nhiên cho enemy
        int randomColor = Random.Range(0, 3); // 0: Xanh, 1: Đỏ, 2: Vàng
        switch (randomColor)
        {
            case 0:
                enemyColor = "Red";
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                enemyColor = "Green";
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                enemyColor = "Yellow";
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
        }
    }
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
            SetColor();
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.EndGame();
        }

    }
}
