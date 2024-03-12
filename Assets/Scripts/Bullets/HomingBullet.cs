using UnityEngine;

public class HomingBullet : Bullet
{
    [SerializeField] private float _rotationSpeed = 100f;
    private Vector3 _direction;

    private void Update()
    {
        _direction = (_target.position - transform.position).normalized;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        transform.position += transform.up * _speed * Time.deltaTime;
    }
}
