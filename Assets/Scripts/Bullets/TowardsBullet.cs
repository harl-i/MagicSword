using System;
using UnityEngine;

public class TowardsBullet : Bullet
{
    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
