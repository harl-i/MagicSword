using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FaliingRock : MonoBehaviour
{
    [SerializeField] private float _delayBeforeDisable;

    private void OnEnable()
    {
        StartCoroutine(DisableAfterDelay(_delayBeforeDisable));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}
