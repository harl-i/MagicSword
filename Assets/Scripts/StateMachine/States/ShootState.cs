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
        switch (_enemyType)
        {
            case ShootingEnemyType.Spider:
            case ShootingEnemyType.Scorpion:
                FlipToPlayer(MoveDirection.Vertical);
                break;
            case ShootingEnemyType.Archer:
                FlipToPlayer(MoveDirection.Horizontal);
                break;
            default:
                break;
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

    private void FlipToPlayer(MoveDirection moveDirection)
    {
        Vector2 directionToPlayer = Player.position - transform.position;
        directionToPlayer.Normalize();

        switch (moveDirection)
        {
            case MoveDirection.Horizontal:
                FlipX(directionToPlayer);
                break;
            case MoveDirection.Vertical:
                FlipY(directionToPlayer);
                break;
            default:
                break;
        }

    }

    private void FlipX(Vector2 directionToPlayer)
    {
        if (directionToPlayer.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (directionToPlayer.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void FlipY(Vector2 directionToPlayer)
    {
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