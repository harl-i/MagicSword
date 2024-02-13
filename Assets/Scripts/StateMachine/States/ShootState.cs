using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ShootState : State
{
    [SerializeField] private Shooting _shooting;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        FlipToPlayer();
        _animator.SetTrigger("Shoot");
        _shooting.enabled = true;
    }

    private void OnDisable()
    {
        _shooting.enabled = false;
    }

    private void FlipToPlayer()
    {
        Vector2 directionToPlayer = Player.transform.position - transform.position;
        directionToPlayer.Normalize();

        if (directionToPlayer.y > 0)
        {
            _spriteRenderer.flipX = true; 
        }
        else if (directionToPlayer.y < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
