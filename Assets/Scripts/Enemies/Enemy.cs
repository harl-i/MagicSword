using UnityEngine;

public class Enemy : MonoBehaviour, IDamaging, IDamageable
{
    [SerializeField] private SpriteBlink _spriteBlink;

    public void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }

    public void TakeDamage()
    {
        //если будет логика принятия урона
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out Player player);

        if (player != null && player.IsLaunched == true)
        {
            TakeDamage();
        }
        else if (player != null && player.IsLaunched == false)
        {
            ApplyDamage(player);
        }
    }
}
