using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shooting : BulletPool
{
    [SerializeField] private ShootingEnemyType _enemyType;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private TowardsBullet _towardsBullet;
    [SerializeField] private StraightBullet _straightBullet;
    [SerializeField] private HomingBullet _homingBullet;
    [SerializeField] private ShowTurret _showTurretComponent;

    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        switch (_enemyType)
        {
            case ShootingEnemyType.Spider:
                Initialize(_straightBullet);
                break;
            case ShootingEnemyType.Gargoyle:
                Initialize(_homingBullet);
                break;
            case ShootingEnemyType.Turret:
                Initialize(_towardsBullet);
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        switch (_enemyType)
        {
            case ShootingEnemyType.Spider:
                SetShootPointPosition();
                break;
            case ShootingEnemyType.Turret:
                _showTurretComponent.enabled = true;
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        switch (_enemyType)
        {
            case ShootingEnemyType.Spider:
                break;
            case ShootingEnemyType.Gargoyle:
                break;
            case ShootingEnemyType.Turret:
                _showTurretComponent.enabled = false;
                break;
            default:
                break;
        }
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger("Shoot");
    }

    public void Shoot()
    {
        switch (_enemyType)
        {
            case ShootingEnemyType.Spider:
                ShootWithStraightBullet();
                break;
            case ShootingEnemyType.Gargoyle:
                ShootWithHomingBullet();
                break;
            case ShootingEnemyType.Turret:
                ShootWithTowardsBullet();
                break;
            default:
                break;
        }
    }
    
    public void FlipX()
    {
        Vector2 position = _shootPoint.localPosition;
        position.x *= -1;
        _shootPoint.localPosition = position;
    }

    public void SetEnemyType(ShootingEnemyType enemyType)
    {
        _enemyType = enemyType;
    }

    public void SetPlayerTransform(Transform transform)
    {
        _playerTransform = transform;
    }

    private void ShootWithStraightBullet()
    {
        Bullet bullet = GetBulletFromPool();

        bullet.SetFlip(_spriteRenderer.flipX);
        bullet.gameObject.SetActive(true);
    }

    private void ShootWithTowardsBullet()
    {
        Bullet bullet = GetBulletFromPool();
        bullet.SetTarget(_playerTransform);
        bullet.CalculateDirection();
        bullet.gameObject.SetActive(true);
    }

    private void ShootWithHomingBullet()
    {
        Bullet bullet = GetBulletFromPool();

        bullet.SetTarget(_playerTransform);
        bullet.gameObject.SetActive(true);
    }

    private Bullet GetBulletFromPool()
    {
        TryGetObject(out Bullet bullet);
        bullet.transform.position = _shootPoint.position;
        return bullet;
    }

    private void SetShootPointPosition()
    {
        if (!_spriteRenderer.flipX && _shootPoint.localPosition.x <= 0 || _spriteRenderer.flipX && _shootPoint.localPosition.x >= 0)
        {
            FlipX();
        }
    }
}
