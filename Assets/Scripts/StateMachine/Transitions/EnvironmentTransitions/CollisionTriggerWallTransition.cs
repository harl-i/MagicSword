using UnityEngine;

public class CollisionTriggerWallTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            NeedTransit = true;
        }
    }
}
