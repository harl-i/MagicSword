using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeReference] private GameObject _enemy;

    private IAttackable _attackableEnemy;

    private void Awake()
    {
        _attackableEnemy = _enemy.GetComponent<IAttackable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player magicSword) && _attackableEnemy != null)
            _attackableEnemy.StartAttack();
    }
}
