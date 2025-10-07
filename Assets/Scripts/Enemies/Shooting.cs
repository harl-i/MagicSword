using Bullets;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public abstract class Shooting : BulletPool
    {
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected TowardsBullet TowardsBullet;
        [SerializeField] protected StraightBullet StraightBullet;
        [SerializeField] protected HomingBullet HomingBullet;
        [SerializeField] protected ShowTurret ShowTurretComponent;

        private Animator _animator;
        private float _minAngleLeftSide = -45f;
        private float _maxAngleLeftSide = 55f;

        private float _maxPositiveAngleRightSide = 130f;
        private float _minPositiveAngleRightSide = 180f;
        private float _maxNegativeAngleRightSide = -180f;
        private float _minNegativeAngleRightSide = -140f;

        private float _rotationSpeed = 150f;

        protected SpriteRenderer SpriteRenderer;
        protected Transform PlayerTransform;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        protected void InitializePool(Bullet prefab)
        {
            Initialize(prefab);
        }

        protected IEnumerator LookAtPlayerAndShoot()
        {
            WaitForSeconds wait = new WaitForSeconds(Time.deltaTime);
            WaitForSeconds delayBeforeShoot = new WaitForSeconds(0.5f);
            while (true)
            {
                Vector2 direction = (Vector2)PlayerTransform.position - (Vector2)transform.position;
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

        protected void ShootWithStraightBullet()
        {
            Bullet bullet = GetBulletFromPool();
            if (bullet == null) return;

            bullet.SetFlip(SpriteRenderer.flipX);
            bullet.gameObject.SetActive(true);
        }

        protected void ShootWithTowardsBullet(bool canLookAtTarget)
        {
            Bullet bullet = GetBulletFromPool();
            if (bullet == null) return;

            bullet.SetTarget(PlayerTransform);

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

            bullet.SetTarget(PlayerTransform);
            bullet.gameObject.SetActive(true);
        }

        protected Bullet GetBulletFromPool()
        {
            TryGetObject(out Bullet bullet);

            if (bullet != null)
            {
                bullet.transform.position = ShootPoint.position;
                return bullet;
            }

            return null;
        }

        protected void SetShootPointPosition()
        {
            Debug.Log(ShootPoint.localPosition.x);
            if (!SpriteRenderer.flipX && ShootPoint.localPosition.x <= 0 || SpriteRenderer.flipX && ShootPoint.localPosition.x >= 0)
            {
                FlipX();
            }
        }

        public void PlayShootAnimation()
        {
            _animator.SetTrigger("Shoot");
        }

        public abstract void Shoot();

        public void FlipX()
        {
            Vector2 position = ShootPoint.localPosition;
            position.x *= -1;
            ShootPoint.localPosition = position;
        }

        public void SetPlayerTransform(Transform transform)
        {
            PlayerTransform = transform;
        }
    }
}