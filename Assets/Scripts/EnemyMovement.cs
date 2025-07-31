using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _reachThreshold = 0.2f;

    private EnemyPath _path;
    private int _currentWaypointIndex;
    private Transform _currentWaypoint;

    private void Update()
    {
        if (_path == null)
            return;

        MoveTowardsWaypoint();
        RotateTowardsWaypoint();
        CheckWaypointReached();
    }

    public void Initialize(EnemyPath path)
    {
        if (path == null || path.Count == 0)
            return;

        _path = path;
        _currentWaypointIndex = 0;
        UpdateCurrentWaypoint();
        transform.LookAt(_currentWaypoint.position);
    }

    private void MoveTowardsWaypoint()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _currentWaypoint.position,
            _speed * Time.deltaTime);
    }

    private void RotateTowardsWaypoint()
    {
        var direction = _currentWaypoint.position - transform.position;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                _rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckWaypointReached()
    {
        float distance = Vector3.Distance(transform.position, _currentWaypoint.position);

        if (distance < _reachThreshold)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _path.Count)
                _currentWaypointIndex = 0;

            UpdateCurrentWaypoint();
        }
    }

    private void UpdateCurrentWaypoint()
    {
        _currentWaypoint = _path.GetWaypoint(_currentWaypointIndex);
    }
}