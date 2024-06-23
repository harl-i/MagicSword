using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RunAttackState : State
{
    [SerializeField] private float _speed;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Attack");
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
}
