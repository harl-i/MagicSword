using UnityEngine;

public class BirdHitTransition : Transition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bird bird))
        {
            NeedTransit = true;
        }
    }
}
