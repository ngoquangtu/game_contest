using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Chọn vị trí spawn ngẫu nhiên
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject selectedEnemy = enemyPrefab[Random.Range(0,enemyPrefab.Length)];
            // Tạo enemy tại vị trí spawn
            Instantiate(selectedEnemy, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
