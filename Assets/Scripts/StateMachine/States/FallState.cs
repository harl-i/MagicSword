using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class FallState : State
{
    private PolygonCollider2D _colliderForDisable;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _colliderForDisable = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Fall");
    }

    public void OnAnmationEnd()
    {
        _colliderForDisable.enabled = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
