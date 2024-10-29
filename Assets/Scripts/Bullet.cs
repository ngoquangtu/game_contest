using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public string bulletColor; 

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.enemyColor == bulletColor)
            {
                Destroy(other.gameObject);
                Destroy(gameObject); 
                GameManager.Instance.AddScore(1);
            }
        }
    }
}
