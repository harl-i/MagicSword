using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Shooting))]
public class ShootState : State
{
    [SerializeField] private ShootingEnemyType _enemyType;
    [SerializeField] private Shooting _shootingComponent;
    [SerializeField] private float _shootDelay;

    private SpriteRenderer _spriteRenderer;
    private float _shootElapsedTime = 0f;

    public ShootingEnemyType EnemyType => _enemyType;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shootElapsedTime = _shootDelay;
        _shootingComponent.SetEnemyType(_enemyType);
    }

    private void OnEnable()
    {
        if (_enemyType == ShootingEnemyType.Spider ||
            _enemyType == ShootingEnemyType.Scorpion)
        {
            FlipToPlayer();
        }

        _shootingComponent.enabled = true;
        _shootingComponent.SetPlayerTransform(Player);
    }

    private void OnDisable()
    {
        _shootingComponent.enabled = false;
    }

    private void Update()
    {
        _shootElapsedTime += Time.deltaTime;
        if (_shootElapsedTime >= _shootDelay)
        {
            _shootingComponent.PlayShootAnimation();
            _shootElapsedTime = 0f;
        }
    }

    private void FlipToPlayer()
    {
        Vector2 directionToPlayer = Player.position - transform.position;
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