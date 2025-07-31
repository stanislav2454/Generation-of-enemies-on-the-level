using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyLifetime))]
public class Enemy : MonoBehaviour
{
    public EnemyMovement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();
    }
}