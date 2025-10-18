using UnityEngine;

namespace Bullets
{
    public class StraightBullet : Bullet
    {
        private void Update()
        {
            if (IsFlip)
            {
                transform.Translate(new Vector2(0, _speed * Time.deltaTime), Space.Self);
            }
            else
            {
                transform.Translate(new Vector2(0, _speed * Time.deltaTime * -1), Space.Self);
            }
        }
    }
}