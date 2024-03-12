using UnityEngine;

public class Shooting : BulletPool
{
    [SerializeField] private ShootingEnemyType _enemyType;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _secondsBetweenShoot;
    [SerializeField] private TowardsBullet _towardsBullet;
    [SerializeField] private StraightBullet _straightBullet;
    [SerializeField] private HomingBullet _homingBullet;

    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

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
        if (_enemyType == ShootingEnemyType.Spider)
        {
            SetShootPointPosition();
        }
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
    }

    private void ShootWithTowardsBullet()
    {
        Bullet bullet = GetBulletFromPool();

        bullet.SetTarget(_playerTransform);
    }

    private void ShootWithHomingBullet()
    {
        Bullet bullet = GetBulletFromPool();

        bullet.SetTarget(_playerTransform);
    }

    private Bullet GetBulletFromPool()
    {
        TryGetObject(out Bullet bullet);
        bullet.gameObject.SetActive(true);
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
