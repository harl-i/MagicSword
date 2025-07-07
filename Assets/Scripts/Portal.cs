using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Animator))]
public class Portal : MonoBehaviour
{
    [SerializeField] private int _soulsAmountForActivation;
    [SerializeField] private PortalActivator _portalActivator;
    [SerializeField] private NextSceneLoader _nextSceneLoader;

    private Animator _animator;
    private int _currentSoulsAmountForActivation;
    private bool _isActive;

    public Action SoulsCollected;
    public Action<int> SoulsChanged;

    public int SoulsAmountForActivation => _soulsAmountForActivation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _portalActivator.PortalActivatorActivated += OnPortalActivatorActivated;
    }

    private void OnDisable()
    {
        _portalActivator.PortalActivatorActivated -= OnPortalActivatorActivated;
    }

    private void OnPortalActivatorActivated()
    {
        _animator.SetTrigger("Activation");
        _isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Soul soul))
        {
            _currentSoulsAmountForActivation++;
            SoulsChanged?.Invoke(_currentSoulsAmountForActivation);

            if (_currentSoulsAmountForActivation == _soulsAmountForActivation)
            {
                SoulsCollected?.Invoke();
                AddSoulsToPlayerSaves();
            }
        }

        if (collision.gameObject.GetComponentInChildren<Player>() != null && _isActive)
        {
            _nextSceneLoader.LoadScene();
        }
    }

    private void AddSoulsToPlayerSaves()
    {
        YG2.saves.soulsCount += _soulsAmountForActivation;
    }
}
