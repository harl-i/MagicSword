using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ShootState : State
{
    [SerializeField] private ShootingEnemyType _enemyType;
    [SerializeField] private Shooting _shootingComponent;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (_enemyType == ShootingEnemyType.Spider)
        {
            FlipToPlayer();
        }

        _animator.SetTrigger("Shoot");
        _shootingComponent.enabled = true;
    }

    private void OnDisable()
    {
        _shootingComponent.enabled = false;
    }

    private void FlipToPlayer()
    {
        Vector2 directionToPlayer = Player.transform.position - transform.position;
        directionToPlayer.Normalize();

        if (directionToPlayer.y > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (directionToPlayer.y < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}

//public enum ShootingEnemyType
//{
//    Spider,
//    Gargoyle,
//    Turret
//}
