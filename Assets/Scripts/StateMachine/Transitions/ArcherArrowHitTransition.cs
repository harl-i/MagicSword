using UnityEngine;

public class ArcherArrowHitTransition : Transition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ArcherArrow archerArrow))
        {
            NeedTransit = true;
        }
    }
}
