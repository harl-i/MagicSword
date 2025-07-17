using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DestructiblePlatform : MonoBehaviour
{
    private PolygonCollider2D _colliderForDisable;
    public List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
    private float _delay = 1;

    private void Awake()
    {
        _colliderForDisable = GetComponent<PolygonCollider2D>();

        foreach (Transform child in transform)
        {
            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                _particleSystems.Add(ps);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _colliderForDisable.enabled = false;

            foreach (ParticleSystem ps in _particleSystems)
                ps.Play();

            StartCoroutine(DelayBeforeDisablePlatform(_delay));
        }
    }

    private IEnumerator DelayBeforeDisablePlatform(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}
