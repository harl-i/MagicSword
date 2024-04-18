using UnityEngine;

public class TrappedInTrolleyTransition : Transition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TrollleyTrap trollleyTrap))
        {
            NeedTransit = true;
        }
    }
}
