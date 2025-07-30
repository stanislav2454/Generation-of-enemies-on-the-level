using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const float MinimumInclusiveRangeValue = -1f;
    const float MaximumInclusiveRangeValue = 1f;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifetime = 3f;

    private Vector3 _direction;

    private void OnEnable()
    {
        Destroy(gameObject, _lifetime);
    }

    public void SetRandomDirection()
    {
        float randomX = Random.Range(MinimumInclusiveRangeValue, MaximumInclusiveRangeValue);
        float randomZ = Random.Range(MinimumInclusiveRangeValue, MaximumInclusiveRangeValue);

        _direction = new Vector3(randomX, 0f, randomZ).normalized;
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }
}
