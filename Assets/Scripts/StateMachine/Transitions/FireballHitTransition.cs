using UnityEngine;

public class FireballHitTransition : Transition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TurretFireball turretFireball))
        {
            NeedTransit = true;
        }
    }
}
