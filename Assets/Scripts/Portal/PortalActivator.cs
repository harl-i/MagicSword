using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PortalActivator : MonoBehaviour
{
    [SerializeField] private Portal _portal;

    private Animator _animator;
    private bool _isActive;

    public Action PortalActivatorActivated;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isActive = false;
    }

    private void OnEnable()
    {
        _portal.SoulsCollected += OnSoulsCollected;
    }

    private void OnDisable()
    {
        _portal.SoulsCollected -= OnSoulsCollected;
    }

    private void OnSoulsCollected()
    {
        _animator.SetTrigger("Activation");
        _isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && _isActive)
        {
            PortalActivatorActivated?.Invoke();
        }
    }
}
