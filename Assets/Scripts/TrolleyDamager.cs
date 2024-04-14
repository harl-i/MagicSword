using UnityEngine;

public class TrolleyDamager : MonoBehaviour, IDamaging
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            ApplyDamage(player);
        }
    }

    public void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }
}
