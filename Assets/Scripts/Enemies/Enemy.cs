using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Enemy : MonoBehaviour, IDamaging
{
    [SerializeField] private float _enableColliderDelay = 1.5f;

    private PolygonCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
    }

    public void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out Player player);
        if (player != null)
        {
            if (!player.IsLaunched)
            {
                ApplyDamage(player);
                StartCoroutine(TemporarilyDisableCollider());
            }
        }
    }

    private IEnumerator TemporarilyDisableCollider()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(_enableColliderDelay);
        _collider.enabled = true;
    }
}
