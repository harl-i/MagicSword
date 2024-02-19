using UnityEngine;

public class PlayerAttackTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.IsLaunched)
            {
                NeedTransit = true;
            }
        }
        else if(collision.TryGetComponent(out Shield shield))
        {
            NeedTransit = true;
        }
    }
}
