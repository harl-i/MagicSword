using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class TrapWaitState : State
{
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Idle");
        _boxCollider2D.enabled = true;
    }
}
