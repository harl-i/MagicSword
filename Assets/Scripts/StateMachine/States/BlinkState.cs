using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlinkState : State
{
    [SerializeField] private PolygonCollider2D _colliderForDisable;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _colliderForDisable.enabled = false; 
        _animator.SetTrigger("Blink");
    }
}
