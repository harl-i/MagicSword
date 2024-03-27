using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class PatrolState : State
{
    [SerializeField] private MoveDirection _moveDirection;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _colliderOffsetX;

    private int waypointIndex = 0;
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _collider;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Patrol");
    }

    private void Start()
    {
        MoveToNextWaypoint();
    }

    private void Update()
    {
        if (ReachedCurrentWaypoint())
        {
            MoveToNextWaypoint();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[waypointIndex].position, _speed * Time.deltaTime);

        Vector2 direction = (_waypoints[waypointIndex].position - transform.position).normalized;
        //if (_moveDirection == MoveDirection.Vertical)
        //{
        //    _spriteRenderer.flipX = direction.y > 0;
        //}
        //else if (_moveDirection == MoveDirection.Horizontal)
        //{
        //    _spriteRenderer.flipX = direction.x < 0;
        //}

        switch (_moveDirection)
        {
            case MoveDirection.Horizontal:
                _spriteRenderer.flipX = direction.x < 0;
                _collider.offset = new Vector2(_spriteRenderer.flipX ? _colliderOffsetX : 0, _collider.offset.y);
                break;
            case MoveDirection.Vertical:
                _spriteRenderer.flipX = direction.y > 0;
                break;
            default:
                break;
        }

        
    }

    private bool ReachedCurrentWaypoint()
    {
        return Vector2.Distance(transform.position, _waypoints[waypointIndex].position) < 0.5f;
    }

    private void MoveToNextWaypoint()
    {
        waypointIndex = (waypointIndex + 1) % _waypoints.Length;
    }
}

public enum MoveDirection
{
    Horizontal,
    Vertical
}