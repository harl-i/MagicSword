using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class RunAttackState : State
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _moveDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        FlipToPlayer();
        _animator.SetTrigger("Attack");
    }

    private void Update()
    {
        Run();
    }

    private void OnDisable()
    {
        _animator.ResetTrigger("Attack");
    }

    private void Run()
    {
        transform.Translate(Vector2.right * _speed * _moveDirection * Time.deltaTime);
    }

    private void FlipToPlayer()
    {
        Vector2 directionToPlayer = Player.transform.position - transform.position;
        directionToPlayer.Normalize();

        if (directionToPlayer.x > 0)
        {
            _moveDirection = 1;
            _spriteRenderer.flipX = false;
        }
        else if (directionToPlayer.x < 0)
        {
            _moveDirection = -1;
            _spriteRenderer.flipX = true;
        }
    }
}
