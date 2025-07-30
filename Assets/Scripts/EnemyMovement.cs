using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }
    // два варианта движения: в указанном или рандомном направлнии
    public void SetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        direction = new Vector3(randomX, 0f, randomZ).normalized;
    }

    private void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
