using UnityEngine;

public class HomingBullet : Bullet
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _lifetime;

    //private Vector3 _direction;
    private float _elapsedTime;

    private void OnEnable()
    {
        _elapsedTime = 0f;
    }

    private void Update()
    {
        //_direction = (_target.position - transform.position).normalized;
        CalculateDirection();
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        transform.position += transform.up * _speed * Time.deltaTime;

        if (_elapsedTime >= _lifetime)
        {
            gameObject.SetActive(false);
        }

        _elapsedTime += Time.deltaTime;
    }
}
