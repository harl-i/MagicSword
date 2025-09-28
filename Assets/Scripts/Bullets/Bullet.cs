using Sword;
using UnityEngine;

namespace Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float _speed;

        protected Transform Target;
        protected bool IsFlip;
        protected Vector3 Direction;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage();
            }

            gameObject.SetActive(false);
        }

        public void SetDirection(Vector3 direction)
        {
            Direction = direction;
        }

        public void CalculateDirection()
        {
            if (Target != null)
            {
                Direction = (Target.position - transform.position).normalized;
            }
        }

        public void LookAtTarget()
        {
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void SetFlip(bool isFlip)
        {
            IsFlip = isFlip;
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}