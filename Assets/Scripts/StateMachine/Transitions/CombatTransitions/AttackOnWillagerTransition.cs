using UnityEngine;

public class AttackOnWillagerTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Willager willager))
        {
            NeedTransit = true;
        }
    }
}