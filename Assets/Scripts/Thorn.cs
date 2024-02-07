using UnityEngine;

public class Thorn : MonoBehaviour, IDamaging
{
    public void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            ApplyDamage(player);
        }
    }
}
