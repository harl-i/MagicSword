using UnityEngine;

[RequireComponent(typeof(SwordThrow))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private SpriteBlink _spriteBlink;

    private bool _isLaunched;
    private SwordThrow _swordThrow;

    public bool IsLaunched => _isLaunched;

    private void Awake()
    {
        _swordThrow = GetComponent<SwordThrow>();
    }

    private void OnEnable()
    {
        _swordThrow.SwordLaunched += OnSwordLaunched;
    }

    private void OnDisable()
    {
        _swordThrow.SwordLaunched -= OnSwordLaunched;
    }

    public void TakeDamage()
    {
        _spriteBlink.enabled = true;
    }

    private void OnSwordLaunched(bool isLaunched)
    {
        _isLaunched = isLaunched;
    }
}
