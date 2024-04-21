using UnityEngine;

public class AttackOnWillagerTransition : Transition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Willager willager))
        {
            NeedTransit = true;
        }
    }
}