using System.Collections;
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
    [SerializeField] private LookAtBullet _lookAtBullet;

    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private Animator _animator;
    public float _minAngle = -45f;
    public float _maxAngle = 55f;
    private float _rotationSpeed = 150f;

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
            case ShootingEnemyType.Scorpion:
                Initialize(_homingBullet);
                break;
            case ShootingEnemyType.Turret:
                Initialize(_towardsBullet);
                break;
            case ShootingEnemyType.TowardsTurret:
                Initialize(_lookAtBullet);
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
            case ShootingEnemyType.Scorpion:
                SetShootPointPosition();
                break;
            case ShootingEnemyType.Turret:
            case ShootingEnemyType.TowardsTurret:
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
            case ShootingEnemyType.Scorpion:
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
            case ShootingEnemyType.Scorpion:
                ShootWithHomingBullet();
                break;
            case ShootingEnemyType.Turret:
                ShootWithTowardsBullet();
                break;
            case ShootingEnemyType.TowardsTurret:
                StartCoroutine(LookAtPlayerAndShoot());
                break;
            default:
                break;
        }
    }

    private IEnumerator LookAtPlayerAndShoot()
    {
        WaitForSeconds wait = new WaitForSeconds(Time.deltaTime);
        WaitForSeconds delayBeforeShoot = new WaitForSeconds(0.5f);
        while (true)
        {
            Vector2 direction = (Vector2)_playerTransform.position - (Vector2)transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            targetAngle = Mathf.Clamp(targetAngle, _minAngle, _maxAngle);

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                yield return delayBeforeShoot;
                //ShootWithLookAtBullet(new Vector2(transform.forward.z, transform.forward.y));

                ShootWithLookAtBullet(transform.right);

                Debug.Log(transform.right);
                yield break;
            }

            yield return wait;
        }
    }

    private void ShootWithLookAtBullet(Vector3 direction)
    {
        Bullet bullet = GetBulletFromPool();
        bullet.SetDirection(direction);
        bullet.gameObject.SetActive(true);
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
