using System.Collections;
using UnityEngine;

public class TrolleyDamager : MonoBehaviour, IDamaging
{
    [SerializeField] private float _enableColliderDelay = 1.5f;
    [SerializeField] private PolygonCollider2D _colliderForDisable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            ApplyDamage(player);
            StartCoroutine(TemporaryDisableCollider());
        }
    }

    public void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }

    private IEnumerator TemporaryDisableCollider()
    {
        _colliderForDisable.enabled = false;
        yield return new WaitForSeconds(_enableColliderDelay);
        _colliderForDisable.enabled = true;
    }
}
