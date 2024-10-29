using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    public string enemyColor; // Xanh, Đỏ hoặc Vàng

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

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
