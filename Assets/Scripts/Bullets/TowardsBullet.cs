using System;
using UnityEngine;

namespace Bullets
{
    public class TowardsBullet : Bullet
    {
        private void Update()
        {
            transform.position += _direction * _speed * Time.deltaTime;
        }
    }
}