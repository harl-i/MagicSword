using Bullets;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public abstract class Shooting : BulletPool
    {
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
        }

        public void PlayShootAnimation()
        {
            _animator.SetTrigger("Shoot");
        }

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

        public void SetPlayerTransform(Transform transform)
        {
            _playerTransform = transform;
        }

        protected void ShootWithStraightBullet()
        {
            Bullet bullet = GetBulletFromPool();
            if (bullet == null) return;

            bullet.SetFlip(_spriteRenderer.flipX);
            bullet.gameObject.SetActive(true);
        }

        protected void ShootWithTowardsBullet(bool canLookAtTarget)
        {
            Bullet bullet = GetBulletFromPool();
            if (bullet == null) return;

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
            if (bullet == null) return;

            bullet.SetDirection(direction);
            bullet.gameObject.SetActive(true);
        }

        protected void ShootWithHomingBullet()
        {
            Bullet bullet = GetBulletFromPool();
            if (bullet == null) return;

            bullet.SetTarget(_playerTransform);
            bullet.gameObject.SetActive(true);
        }

        protected Bullet GetBulletFromPool()
        {
            TryGetObject(out Bullet bullet);

            if (bullet != null)
            {
                bullet.transform.position = _shootPoint.position;
                return bullet;
            }

            return null;
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