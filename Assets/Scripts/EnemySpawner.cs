using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; 
    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private float spawnInterval = 2f; 

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled) 
        {
            yield return new WaitForSeconds(spawnInterval); 

            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();

            if (enemyMovement != null)
            {
                // два варианта движения: в указанном или рандомном направлнии
                enemyMovement.SetRandomDirection();
                //enemyMovement.SetDirection(randomSpawnPoint.forward);
            }
        }
    }
}