using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class BlinkState : State
{
    private PolygonCollider2D _colliderForDisable;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _colliderForDisable = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        _colliderForDisable.enabled = false; 
        _animator.SetTrigger("Blink");
    }
}
