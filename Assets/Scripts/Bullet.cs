using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public string bulletColor;
    private SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip clip;
    private bool hasHitEnemy = false;
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
        if(other.CompareTag("Enemy2"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            {
                if (enemy.enemyColor == bulletColor)
            {
                    Destroy(gameObject);
                    enemy.TakeHit();
            }
            }
        }
    }
    public Color GetColor()
    {
        return spriteRenderer.color;
    }
    private void PlaySoundAndDestroy()
    {
        if (!hasHitEnemy)
        {
            hasHitEnemy = true;  // Đảm bảo chỉ gọi hủy một lần
            audioSource.PlayOneShot(clip);
            Invoke("DestroyBullet", clip.length);  // Hủy đạn sau khi âm thanh phát xong
        }
    }

}
