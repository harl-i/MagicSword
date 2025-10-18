using UnityEngine;

namespace Bullets
{
    public class TowardsBullet : Bullet
    {
        private void Update() => transform.position += (_speed * Time.deltaTime) * Direction;
    }
}
