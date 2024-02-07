using UnityEngine;

public class PlayerAttackTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Transition trigger!");
        if (collision.TryGetComponent(out Player player) && player.IsLaunched == true)
        {
            NeedTransit = true;
        }
    }
}
