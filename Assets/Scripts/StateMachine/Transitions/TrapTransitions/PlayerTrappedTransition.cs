using UnityEngine;

public class PlayerTrappedTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            NeedTransit = true;
        }
        else if (collision.TryGetComponent(out Shield shield))
        {
            NeedTransit = true;
        }
    }
}
