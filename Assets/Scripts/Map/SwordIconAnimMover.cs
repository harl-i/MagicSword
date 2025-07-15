using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordIconAnimMover : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Animator _animator;

    public bool IsMoveCompleted { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        IsMoveCompleted = false;
    }

    public IEnumerator StartAnimationAfterDelay()
    {
        yield return new WaitForSeconds(_delay);

        _animator.SetTrigger("MoveToNextPoint");
    }
}
