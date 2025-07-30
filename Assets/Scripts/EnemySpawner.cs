using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private EnemyMovement _enemyPrefab;

    private Coroutine _spawningCoroutine;
    private WaitForSeconds _spawnWait;

    private void OnEnable()
    {
        _spawnWait = new WaitForSeconds(spawnInterval);
        _spawningCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawningCoroutine);
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            EnemyMovement newEnemy = Instantiate(_enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();

            if (enemyMovement != null)
                enemyMovement.SetRandomDirection();

            yield return _spawnWait;
        }
    }
}