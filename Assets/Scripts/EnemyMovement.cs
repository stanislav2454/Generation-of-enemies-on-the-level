using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //const float MinimumInclusiveRangeValue = -1f;
    //const float MaximumInclusiveRangeValue = 1f;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private float _reachThreshold = 0.2f;

    private List<Transform> _waypoints = new List<Transform>();
    private int _currentWaypointIndex = 0;

    //private Vector3 _direction;

    private void OnEnable()
    {
        Destroy(gameObject, _lifetime);
    }

    public void Initialize(Transform[] waypoints)
    {
        _waypoints = new List<Transform>(waypoints);
        _currentWaypointIndex = 0;

        // Начинаем движение к первому waypoint
        if (_waypoints.Count > 0)
        {
            transform.LookAt(_waypoints[_currentWaypointIndex].position);
        }
    }

    //public void SetRandomDirection()
    //{
    //    float randomX = Random.Range(MinimumInclusiveRangeValue, MaximumInclusiveRangeValue);
    //    float randomZ = Random.Range(MinimumInclusiveRangeValue, MaximumInclusiveRangeValue);

    //    _direction = new Vector3(randomX, 0f, randomZ).normalized;
    //}

    private void Update()
    {
        if (_waypoints.Count == 0) return;

        Transform currentWaypoint = _waypoints[_currentWaypointIndex];

        // Движение к waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentWaypoint.position,
            _speed * Time.deltaTime
        );

        // Поворот в сторону движения
        if ((transform.position - currentWaypoint.position).sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                currentWaypoint.position - transform.position
            );
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * 5f
            );
        }

        // Проверка достижения waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < _reachThreshold)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
        }
        //transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }
}
