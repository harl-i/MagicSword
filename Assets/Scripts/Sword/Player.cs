using UnityEngine;

[RequireComponent(typeof(SwordMovingState))]
public class Player : MonoBehaviour, IDamageable
{
    private bool _isLaunched;
    private SwordMovingState _swordMovingState;

    public bool IsLaunched => _isLaunched;

    private void Awake()
    {
        _swordMovingState = GetComponent<SwordMovingState>();
    }

    private void OnEnable()
    {
        _swordMovingState.SwordLaunched += OnSwordLaunched;
    }

    private void OnDisable()
    {
        _swordMovingState.SwordLaunched -= OnSwordLaunched;
    }

    public void TakeDamage()
    {
        
    }

    private void OnSwordLaunched(bool isLaunched)
    {
        _isLaunched = isLaunched;
    }
}
