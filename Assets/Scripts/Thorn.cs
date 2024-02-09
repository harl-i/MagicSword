using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Thorn : MonoBehaviour, IDamaging
{
    private float _delayEnableTrigger = 5f;
    private PolygonCollider2D _triggerCollider;

    private void Awake()
    {
        _triggerCollider = GetComponent<PolygonCollider2D>();
    }

    public void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            ApplyDamage(player);
            TemporaryDisableTrigger();
        }
    }

    private void TemporaryDisableTrigger()
    {
        _triggerCollider.enabled = false;

        StartCoroutine(EnableColliderAfterDelay(_delayEnableTrigger));
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _triggerCollider.enabled = true;
    }
}
