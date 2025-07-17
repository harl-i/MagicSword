using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeAttackState : State
{
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private float _raycastLength;
    [SerializeField] private float _raycastOriginOffsetX = 0f;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Transform _raycastOriginStartPosition;
    private Vector2 _raycastOriginOffsetXPosition;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _raycastOriginStartPosition = _raycastOrigin;
        _raycastOriginOffsetXPosition = new Vector2(_raycastOriginStartPosition.position.x + _raycastOriginOffsetX, _raycastOriginStartPosition.position.y);
    }

    private void OnEnable()
    {
        SetRaycastOriginPosition();
        _animator.SetTrigger("Attack");
    }

    private void OnDisable()
    {
        _animator.ResetTrigger("Attack");
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
            _raycastOrigin.position = _raycastOriginOffsetXPosition;
        }
        else if (directionToPlayer.x > 0)
        {
            _spriteRenderer.flipX = false;
            _raycastOrigin = _raycastOriginStartPosition;
        }
    }

    private void SetRaycastOriginPosition()
    {
        if ((!_spriteRenderer.flipX && _raycastOrigin.localPosition.x <= 0) || (_spriteRenderer.flipX && _raycastOrigin.localPosition.x >= 0))
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
