using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform[] _waypoints;
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
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            EnemyMovement newEnemy = Instantiate(_enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            //if (newEnemy != null)
            //    newEnemy.SetRandomDirection();

            if (newEnemy != null && _waypoints.Length > 0)
            {
                // Передаем waypoints врагу
                newEnemy.Initialize(_waypoints);
            }

            yield return _spawnWait;
        }
    }
}