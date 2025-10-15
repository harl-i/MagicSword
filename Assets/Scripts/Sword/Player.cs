using System;
using DamageInterfaces;
using UnityEngine;
using YG;

namespace Sword
{
    [RequireComponent(typeof(SwordMovingState))]
    [RequireComponent(typeof(ShieldActivator))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour, IDamageable
    {
        private SwordMovingState _swordMovingState;
        private ShieldActivator _shieldActivator;
        private Animator _animator;

        public static Action<int> HealthHasChanged;

        public bool IsLaunched { get; private set; }
        public bool IsShieldActivated { get; private set; }
        public int Health { get; private set; } = 3;

        private void Awake()
        {
            _swordMovingState = GetComponent<SwordMovingState>();
            _shieldActivator = GetComponent<ShieldActivator>();
            _animator = GetComponent<Animator>();
        }

        private void Start() => HealthHasChanged?.Invoke(Health);

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

            if (Health > 0 && !IsShieldActivated)
            {
                Health--;
                HealthHasChanged?.Invoke(Health);
            }
        }

        public void FullHealing()
        {
            Health = 3;
            HealthHasChanged?.Invoke(Health);
        }

        private void OnSwordLaunched(bool isLaunched) => IsLaunched = isLaunched;

        private void OnShieldActivated(bool isActivated) => IsShieldActivated = isActivated;
    }
}
