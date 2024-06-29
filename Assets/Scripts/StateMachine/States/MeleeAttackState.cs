using UnityEngine;

[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(PolygonCollider2D))]
public class MeleeAttackState : State
{
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private float _raycastLength;

    private SpriteRenderer _spriteRenderer;
    //private PolygonCollider2D _collider;
    private Animator _animator;
    private float _colliderOffset = 0.61f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        //_collider = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        SetRaycastOriginPosition();
        _animator.SetTrigger("Attack");
    }

    public void Attack()
    {
        FlipToPlayer();

        Vector2 raycastDirection = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(_raycastOrigin.position, raycastDirection, _raycastLength);

        DrawRaycast(_raycastOrigin.position, raycastDirection);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage();
                player.GetComponent<EnemyAttackTransition>().Transition();
            }
        }
    }
    
    private void FlipToPlayer()
    {
        Vector2 directionToPlayer = Player.position - transform.position;
        directionToPlayer.Normalize();

        if (directionToPlayer.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (directionToPlayer.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        //_collider.offset = new Vector2(_spriteRenderer.flipX ? _colliderOffset : 0, _collider.offset.y);
    }

    private void SetRaycastOriginPosition()
    {
        if (!_spriteRenderer.flipX && _raycastOrigin.localPosition.x <= 0 || _spriteRenderer.flipX && _raycastOrigin.localPosition.x >= 0)
        {
            FlipXRaycastOrigin();
        }
    }

    private void FlipXRaycastOrigin()
    {
        Vector2 position = _raycastOrigin.localPosition;
        position.x *= -1;
        _raycastOrigin.localPosition = position;
    }

    private void DrawRaycast(Vector2 raycastOrigin, Vector2 raycastDirection)
    {
        Debug.DrawRay(raycastOrigin, raycastDirection, Color.green);
    }
}
