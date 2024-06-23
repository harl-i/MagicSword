using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StunState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Stun");
    }
}
