using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed;

    protected Transform _target;
    protected bool _isFlip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }

        gameObject.SetActive(false);
    }

    public void SetFlip(bool isFlip)
    {
        _isFlip = isFlip;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
