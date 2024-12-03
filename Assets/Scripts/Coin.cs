using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 10; 

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            GameManager.Instance.AddScore(coinValue);


            Destroy(gameObject);
        }
    }
}
