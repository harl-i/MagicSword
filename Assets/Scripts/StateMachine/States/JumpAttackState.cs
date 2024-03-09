using UnityEngine;

public class JumpAttackState : State
{
    public float _jumpForce = 5f;
    public float _jumpAngle = 45f;
    private float _initialYPosition;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _currentDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        float jumpAngleRad = _jumpAngle * Mathf.Deg2Rad;
        float jumpX = _jumpForce * _currentDirection.x * Mathf.Cos(jumpAngleRad);
        float jumpY = _jumpForce * Mathf.Sin(jumpAngleRad);
        _rigidbody2D.AddForce(new Vector2(jumpX, jumpY), ForceMode2D.Impulse);

        _animator.SetTrigger("Jump");
    }
}
