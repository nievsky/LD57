using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab settings")] public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;
    [Range(0f, 1f)] public float spawnChance = 0.5f;

    [Header("Spawn Area Settings")] public Transform playerSpawnArea;
    public float spawnYMin = 10f;
    public float spawnYMax = 30f;
    public float spawnXMin = -20f;
    public float spawnXMax = 20f;

    void Start()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned to the spawner.");
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // FIXME
        {
            yield return new WaitForSeconds(spawnInterval);
            if (Random.value < spawnChance)
            {
                SpawnEnemy();
            }
        }
    }
    
    void SpawnEnemy()
    {
        Vector2 playerPos = playerSpawnArea.position;
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];
        float spawnX = Random.Range(spawnXMin, spawnXMax);
        float spawnY = playerPos.y - Random.Range(spawnYMin, spawnYMax);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

}
