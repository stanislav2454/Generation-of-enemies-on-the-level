using UnityEngine;

public class EnemyLifetime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 10f;

    private void OnEnable()
    {
        if (_lifetime > 0)
            Destroy(gameObject, _lifetime);
    }
}
