using UnityEngine;

public class Shooting : BulletPool
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _secondsBetweenShoot;
    [SerializeField] private Bullet _bullet;

    private SpriteRenderer _spriteRenderer;
    private float _elapsedTime = 0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize(_bullet);
    }

    private void OnEnable()
    {
        SetShootPointPosition();
    }

    public void Shoot()
    {
        TryGetObject(out Bullet bullet);
        bullet.gameObject.SetActive(true);
        bullet.SetFlip(_spriteRenderer.flipX);
        bullet.transform.position = _shootPoint.position;
    }

    private void SetShootPointPosition()
    {
        if (!_spriteRenderer.flipX && _shootPoint.localPosition.x <= 0 || _spriteRenderer.flipX && _shootPoint.localPosition.x >= 0)
        {
            FlipX();
        }
    }

    public void FlipX()
    {
        Vector2 position = _shootPoint.localPosition;
        position.x *= -1;
        _shootPoint.localPosition = position;
    }

}
