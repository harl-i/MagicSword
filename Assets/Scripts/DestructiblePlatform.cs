using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePlatform : MonoBehaviour 
{
    public List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
    private float _delay = 1;

    private void Awake()
    {
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
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }

        StartCoroutine(DelayBeforeDisablePlatform(_delay));
    }

    private IEnumerator DelayBeforeDisablePlatform(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}
