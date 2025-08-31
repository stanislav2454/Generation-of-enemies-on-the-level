using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _reachThreshold = 0.2f;
    [SerializeField] private float _rotationThreshold = 0.01f;

    private EnemyPath _path;
    private int _currentWaypointIndex;
    private Transform _currentWaypoint;

    private void Update()
    {
        if (_path == null)
            return;

        transform.position = MoveTowardsWaypoint(transform, _currentWaypoint, _speed);
        transform.rotation = RotateTowardsWaypoint(transform, _currentWaypoint, _rotationSpeed, _rotationThreshold);
        _currentWaypoint = CheckWaypointReached(transform, _currentWaypoint, _reachThreshold, _path);
    }

    public void Initialize(EnemyPath path)
    {
        if (path == null || path.Count == 0)
            return;

        _path = path;
        _currentWaypointIndex = 0;
        _currentWaypoint = _path.GetWaypoint(_currentWaypointIndex);
        //_currentWaypoint = UpdateCurrentWaypoint(_path, _currentWaypointIndex);
        transform.LookAt(_currentWaypoint.position);
    }

    private Vector3 MoveTowardsWaypoint(Transform current, Transform currentWaypoint, float speed)
    {
        Vector3 position = Vector3.MoveTowards(
            current.position, currentWaypoint.position, speed * Time.deltaTime);

        return position;
    }

    private Quaternion RotateTowardsWaypoint(
        Transform current, Transform currentWaypoint, float rotationSpeed, float rotationThreshold)
    {
        var direction = currentWaypoint.position - current.position;
        Quaternion rotation = new();

        if (direction.sqrMagnitude > rotationThreshold)
        {
            rotation = Quaternion.Slerp(
               current.rotation,
               Quaternion.LookRotation(direction),
               rotationSpeed * Time.deltaTime);
        }

        return rotation;
    }

    private Transform CheckWaypointReached(
        Transform current, Transform currentWaypoint, float reachThreshold, EnemyPath path)
    {
        float distance = Vector3.Distance(current.position, currentWaypoint.position);

        if (distance < reachThreshold)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= path.Count)
                _currentWaypointIndex = 0;

            return path.GetWaypoint(_currentWaypointIndex);
        }

        return currentWaypoint;
    }


    // метод оказался не нужен после рефакторинга
    //private Transform UpdateCurrentWaypoint(EnemyPath path, int currentWaypointIndex) =>
    //     path.GetWaypoint(currentWaypointIndex);

    // - не надо передавать данные между методами через поля.
    //   Для этого есть входные параметры и возвращаемое значение.

    //private void UpdateCurrentWaypoint()
    //{
    //    _currentWaypoint = _path.GetWaypoint(_currentWaypointIndex);
    //}
}