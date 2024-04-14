using System.Collections;
using UnityEngine;

public class TrolleyTrapColliderTemporaryDisable : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _trolleyTrapCollider;
    [SerializeField] private float _delay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            StartCoroutine(TemporaryDisable(_delay));
        }
    }

    private IEnumerator TemporaryDisable(float delay)
    {
        _trolleyTrapCollider.enabled = false;

        yield return new WaitForSeconds(delay);

        _trolleyTrapCollider.enabled = true;
    }
}
