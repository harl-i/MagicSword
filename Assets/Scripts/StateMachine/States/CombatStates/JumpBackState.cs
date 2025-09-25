using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class JumpBackState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private bool _isReadyToJump;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isReadyToJump)
        {
            MoveBackward();
        }
    }

    private void OnEnable()
    {
        _isReadyToJump = false;
        _animator.SetTrigger("JumpBack");

        _startPosition = transform.position;

        Vector2 direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

        _targetPosition = _startPosition - direction.normalized * _distance;
    }

    private void OnDisable()
    {
        _isReadyToJump = false;
    }

    public void Jump()
    {
        _isReadyToJump = true;
    }

    public void MoveBackward()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_targetPosition, 0.1f);
    }
}
