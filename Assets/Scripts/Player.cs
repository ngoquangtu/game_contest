using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    public TextMeshProUGUI redBulletText;
    public TextMeshProUGUI greenBulletText;
    public TextMeshProUGUI yellowBulletText;
    public AudioSource bulletAudio;
    private string currentBulletColor = "Red";

    public int redBulletCount = 10;
    public int greenBulletCount = 10;
    public int yellowBulletCount = 10;
    public AudioClip bulletSound;

    void Update()
    {
        // Chuyển đổi màu đạn bằng các phím 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Z)) currentBulletColor = "Red";
        if (Input.GetKeyDown(KeyCode.X)) currentBulletColor = "Green";
        if (Input.GetKeyDown(KeyCode.C)) currentBulletColor = "Yellow";

        // Cập nhật giao diện số lượng đạn
        UpdateBulletUI();

        // Bắn đạn bằng phím Space nếu còn đạn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentBulletColor == "Red" && redBulletCount > 0)
        {
            bulletPrefab.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletCount--;
            SpawnBullet();
        }
        else if (currentBulletColor == "Green" && greenBulletCount > 0)
        {
            bulletPrefab.GetComponent<SpriteRenderer>().color = Color.green;
            greenBulletCount--;
            SpawnBullet();
        }
        else if (currentBulletColor == "Yellow" && yellowBulletCount > 0)
        {
            bulletPrefab.GetComponent<SpriteRenderer>().color = Color.yellow;
            yellowBulletCount--;
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        bulletAudio.PlayOneShot(bulletSound);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.bulletColor = currentBulletColor;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
        Destroy(bullet, 5f);
    }

    void UpdateBulletUI()
    {
        redBulletText.text = "Red Bullets: " + redBulletCount;
        greenBulletText.text = "Green Bullets: " + greenBulletCount;
        yellowBulletText.text = "Yellow Bullets: " + yellowBulletCount;
    }
}
