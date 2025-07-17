using System;
using UnityEngine;
using YG;

[RequireComponent(typeof(SwordMovingState))]
[RequireComponent(typeof(ShieldActivator))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDamageable
{
    private int _healthCount = 3;

    private bool _isLaunched;
    private bool _isShieldActivated;

    private SwordMovingState _swordMovingState;
    private ShieldActivator _shieldActivator;
    private Animator _animator;

    public bool IsLaunched => _isLaunched;
    public bool IsShieldActivated => _isShieldActivated;
    public int Health => _healthCount;

    public static Action<int> HealthHasChanged;

    private void Awake()
    {
        _swordMovingState = GetComponent<SwordMovingState>();
        _shieldActivator = GetComponent<ShieldActivator>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        HealthHasChanged?.Invoke(_healthCount);
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
        if (YG2.saves.GodMode == 1)
        {
            return;
        }

        if (_healthCount > 0 && !_isShieldActivated)
        {
            _healthCount--;
            HealthHasChanged?.Invoke(_healthCount);
        }
    }

    public void FullHealing()
    {
        _healthCount = 3;
        HealthHasChanged?.Invoke(_healthCount);
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


