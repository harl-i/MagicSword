using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMover : MonoBehaviour
{
    [SerializeField] private Transform _portal;

    private float _speed = 1.4f;

    private List<Soul> _activeSouls = new List<Soul>();
    private Dictionary<Soul, Action> _soulDisableHandlers = new Dictionary<Soul, Action>();

    private void OnEnable()
    {
        Soul.OnSoulSpawned += HandleSoulSpawned;
    }

    private void OnDisable()
    {
        foreach (var soul in _activeSouls)
        {
            if (_soulDisableHandlers.TryGetValue(soul, out Action handler))
            {
                soul.OnSoulDisabled -= handler;
            }
        }

        _activeSouls.Clear();
        _soulDisableHandlers.Clear();
    }

    private void HandleSoulSpawned(Soul soul)
    {
        _activeSouls.Add(soul);

        Action handler = () => HandleSoulDisabled(soul);
        _soulDisableHandlers[soul] = handler;
        soul.OnSoulDisabled += handler;
    }

    private void HandleSoulDisabled(Soul soul)
    {
        _activeSouls.Remove(soul);
    }

    private void Update()
    {
        for (int i = _activeSouls.Count - 1; i >= 0; i--)
        {
            Soul soul = _activeSouls[i];
            if (soul != null)
            {
                Vector3 direction = (_portal.position - soul.transform.position).normalized;

                if (Vector3.Distance(soul.transform.position, _portal.position) > 0.1f)
                {
                    soul.transform.position += direction * _speed * Time.deltaTime;
                }
            }
        }
    }
}
