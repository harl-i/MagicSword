using Bullets;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public abstract class Shooting : BulletPool
    {
        //[SerializeField] private ShootingEnemyType _enemyType;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] protected TowardsBullet _towardsBullet;
        [SerializeField] protected StraightBullet _straightBullet;
        [SerializeField] protected HomingBullet _homingBullet;
        [SerializeField] protected ShowTurret _showTurretComponent;

        protected SpriteRenderer _spriteRenderer;
        protected Transform _playerTransform;
        private Animator _animator;

        private float _minAngleLeftSide = -45f;
        private float _maxAngleLeftSide = 55f;

        private float _maxPositiveAngleRightSide = 130f;
        private float _minPositiveAngleRightSide = 180f;
        private float _maxNegativeAngleRightSide = -180f;
        private float _minNegativeAngleRightSide = -140f;

        private float _rotationSpeed = 150f;

        protected void InitializePool(Bullet prefab)
        {
            Initialize(prefab);
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            //switch (_enemyType)
            //{
            //    case ShootingEnemyType.Spider:
            //        Initialize(_straightBullet);
            //        break;
            //    case ShootingEnemyType.Gargoyle:
            //    case ShootingEnemyType.Scorpion:
            //        Initialize(_homingBullet);
            //        break;
            //    case ShootingEnemyType.Turret:
            //    case ShootingEnemyType.Archer:
            //        Initialize(_towardsBullet);
            //        break;
            //    case ShootingEnemyType.TowardsTurret:
            //        Initialize(_towardsBullet);
            //        break;
            //    default:
            //        break;
            //}
        }

        private void OnEnable()
        {
            //switch (_enemyType)
            //{
            //    case ShootingEnemyType.Spider:
            //    case ShootingEnemyType.Scorpion:
            //    case ShootingEnemyType.Archer:
            //        SetShootPointPosition();
            //        break;
            //    case ShootingEnemyType.Turret:
            //    case ShootingEnemyType.TowardsTurret:
            //        _showTurretComponent.enabled = true;
            //        break;
            //    default:
            //        break;
            //}
        }

        private void OnDisable()
        {
            //switch (_enemyType)
            //{
            //    case ShootingEnemyType.Turret:
            //    case ShootingEnemyType.TowardsTurret:
            //        _showTurretComponent.enabled = false;
            //        break;
            //    default:
            //        break;
            //}
        }

        public void PlayShootAnimation()
        {
            _animator.SetTrigger("Shoot");
        }

        //public void Shoot()
        //{
        //    //switch (_enemyType)
        //    //{
        //    //    case ShootingEnemyType.Spider:
        //    //        ShootWithStraightBullet();
        //    //        break;
        //    //    case ShootingEnemyType.Gargoyle:
        //    //    case ShootingEnemyType.Scorpion:
        //    //        ShootWithHomingBullet();
        //    //        break;
        //    //    case ShootingEnemyType.Turret:
        //    //        ShootWithTowardsBullet(false);
        //    //        break;
        //    //    case ShootingEnemyType.Archer:
        //    //        ShootWithTowardsBullet(true);
        //    //        break;
        //    //    case ShootingEnemyType.TowardsTurret:
        //    //        StartCoroutine(LookAtPlayerAndShoot());  //private IEnumerator LookAtPlayerAndShoot()
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}
        //}

        public abstract void Shoot();

        public void FlipX()
        {
            Vector2 position = _shootPoint.localPosition;
            position.x *= -1;
            _shootPoint.localPosition = position;
        }

        protected IEnumerator LookAtPlayerAndShoot()
        {
            WaitForSeconds wait = new WaitForSeconds(Time.deltaTime);
            WaitForSeconds delayBeforeShoot = new WaitForSeconds(0.5f);
            while (true)
            {
                Vector2 direction = (Vector2)_playerTransform.position - (Vector2)transform.position;
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (transform.position.x < 0)
                {
                    targetAngle = Mathf.Clamp(targetAngle, _minAngleLeftSide, _maxAngleLeftSide);
                }
                else
                {
                    if (targetAngle > 0)
                    {
                        targetAngle = Mathf.Clamp(targetAngle, _minPositiveAngleRightSide, _maxPositiveAngleRightSide);
                    }
                    else
                    {
                        targetAngle = Mathf.Clamp(targetAngle, _minNegativeAngleRightSide, _maxNegativeAngleRightSide);
                    }
                }

                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    yield return delayBeforeShoot;

                    ShootWithTowardsBullet(transform.right);

                    yield break;
                }

                yield return wait;
            }
        }

        //public void SetEnemyType(ShootingEnemyType enemyType)
        //{
        //    _enemyType = enemyType;
        //}

        public void SetPlayerTransform(Transform transform)
        {
            _playerTransform = transform;
        }

        protected void ShootWithStraightBullet()
        {
            Bullet bullet = GetBulletFromPool();

            bullet.SetFlip(_spriteRenderer.flipX);
            bullet.gameObject.SetActive(true);
        }

        protected void ShootWithTowardsBullet(bool canLookAtTarget)
        {
            Bullet bullet = GetBulletFromPool();
            bullet.SetTarget(_playerTransform);

            bullet.CalculateDirection();

            if (canLookAtTarget)
            {
                bullet.LookAtTarget();
            }

            bullet.gameObject.SetActive(true);
        }

        protected void ShootWithTowardsBullet(Vector3 direction)
        {
            Bullet bullet = GetBulletFromPool();
            bullet.SetDirection(direction);
            bullet.gameObject.SetActive(true);
        }

        protected void ShootWithHomingBullet()
        {
            Bullet bullet = GetBulletFromPool();

            bullet.SetTarget(_playerTransform);
            bullet.gameObject.SetActive(true);
        }

        protected Bullet GetBulletFromPool()
        {
            TryGetObject(out Bullet bullet);
            bullet.transform.position = _shootPoint.position;
            return bullet;
        }

        protected void SetShootPointPosition()
        {
            if (!_spriteRenderer.flipX && _shootPoint.localPosition.x <= 0 || _spriteRenderer.flipX && _shootPoint.localPosition.x >= 0)
            {
                FlipX();
            }
        }
    }
}