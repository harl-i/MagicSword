using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrapWaitState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Idle");
    }
}
