using UnityEngine;

public class BirdHitTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bird bird))
        {
            NeedTransit = true;
        }
    }
}
