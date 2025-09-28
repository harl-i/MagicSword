using UnityEngine;

namespace Bullets
{
    public class TowardsBullet : Bullet
    {
        private void Update()
        {
            transform.position += Direction * _speed * Time.deltaTime;
        }
    }
}