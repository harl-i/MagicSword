using UnityEngine;

[RequireComponent(typeof(SwordMovingState))]
[RequireComponent(typeof(ShieldActivator))]
public class Player : MonoBehaviour, IDamageable
{
    private bool _isLaunched;
    private bool _isShieldActivated;

    private SwordMovingState _swordMovingState;
    private ShieldActivator _shieldActivator;

    public bool IsLaunched => _isLaunched;
    public bool IsShieldActivated => _isShieldActivated;

    private void Awake()
    {
        _swordMovingState = GetComponent<SwordMovingState>();
        _shieldActivator = GetComponent<ShieldActivator>();
    }

    private void OnEnable()
    {
        _swordMovingState.SwordLaunched += OnSwordLaunched;
        _shieldActivator.ShieldActivated += OnShieldActivated;
    }

    private void OnDisable()
    {
        _swordMovingState.SwordLaunched -= OnSwordLaunched;
        _shieldActivator.ShieldActivated -= OnShieldActivated;
    }

    public void TakeDamage()
    {
        
    }

    private void OnSwordLaunched(bool isLaunched)
    {
        _isLaunched = isLaunched;
    }

    private void OnShieldActivated(bool isActivated)
    {
        _isShieldActivated = isActivated;
    }
}
