using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private SpriteBlink _spriteBlink;

    public void TakeDamage()
    {
        _spriteBlink.enabled = true;
    }
}
