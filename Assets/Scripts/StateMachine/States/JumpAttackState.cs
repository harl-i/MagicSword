using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class JumpAttackState : State
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpAngle = 45f;

    private float _initialYPosition;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Jump();
    }

    private void Update()
    {
        if (transform.position.y < _initialYPosition)
        {
            SwitchToKinematic();

            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    }

    public void SwitchToKinematic()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Jump()
    {
        _initialYPosition = transform.position.y;

        FlipToPlayer();

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        float jumpAngleRad = _jumpAngle * Mathf.Deg2Rad;
        float jumpX = _jumpForce * (_spriteRenderer.flipX ? -1 : 1) * Mathf.Cos(jumpAngleRad);
        float jumpY = _jumpForce * Mathf.Sin(jumpAngleRad);
        _rigidbody2D.AddForce(new Vector2(jumpX, jumpY), ForceMode2D.Impulse);

        _animator.SetTrigger("Jump");
    }

    private void FlipToPlayer()
    {
        Vector2 directionToPlayer = Player.transform.position - transform.position;
        directionToPlayer.Normalize();

        if (directionToPlayer.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (directionToPlayer.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
