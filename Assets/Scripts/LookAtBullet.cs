using UnityEngine;

public class LookAtBullet : Bullet
{
    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
