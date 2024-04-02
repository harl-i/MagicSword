using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CollisionWithPlatformsWhenFallingTransition : Transition
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        NeedTransit = false;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void OnDisable()
    {
        NeedTransit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NeedTransit = false;
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            SwitchToKinematic();
            NeedTransit = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        NeedTransit = false;
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            StartCoroutine(TransitAfterDelay(3f));
        }
    }

    private void SwitchToKinematic()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
    }

    private IEnumerator TransitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SwitchToKinematic();
        NeedTransit = true;
    }
}
