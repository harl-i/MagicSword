using UnityEngine;

public class StraightBullet : Bullet
{
    private void Update()
    {
        if (_isFlip)
        {
            transform.Translate(new Vector2(0, _speed * Time.deltaTime), Space.Self);
        }
        else
        {
            transform.Translate(new Vector2(0, _speed * Time.deltaTime * -1), Space.Self);
        }
    }
}
