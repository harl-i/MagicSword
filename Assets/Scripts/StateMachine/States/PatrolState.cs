using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Transform))]
public class PatrolState : State
{
    [SerializeField] private MoveDirection _moveDirection;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private bool _needUpdateWaypointPosition;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _colliderOffsetX;
    [SerializeField] private float _lengthPatrolRoute = 1.7f;

    private int waypointIndex = 0;
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _collider;
    private Animator _animator;
    private Transform _transformForUpdate;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _animator = GetComponent<Animator>();
        _transformForUpdate = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        if (_needUpdateWaypointPosition)
        {
            UpdateWaypointsPositions();
        }

        _animator.SetTrigger("Patrol");

    }

    private void OnDisable()
    {
        _animator.ResetTrigger("Patrol");
    }

    private void UpdateWaypointsPositions()
    {
        //float newPositionY = transform.position.y;
        //foreach (var waypoint in _waypoints)
        //{
        //    Vector2 newPosition = new Vector2(waypoint.position.x, newPositionY);
        //    waypoint.position = newPosition;
        //}


        for (int i = 0; i < _waypoints.Length; i++)
        {
            int directionCoefficient = _transformForUpdate.position.x > 0 ? -1 : 1;

            Vector2 newPosition = new Vector2(
                _transformForUpdate.position.x + i * directionCoefficient * _lengthPatrolRoute, 
                _transformForUpdate.position.y);
            _waypoints[i].position = newPosition;
        }
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

        switch (_moveDirection)
        {
            case MoveDirection.Horizontal:
                _spriteRenderer.flipX = direction.x < 0;
                _collider.offset = new Vector2(_spriteRenderer.flipX ? _colliderOffsetX : 0, _collider.offset.y);
                break;
            case MoveDirection.Vertical:
                _spriteRenderer.flipX = direction.y > 0;
                _collider.offset = new Vector2(_spriteRenderer.flipX ? 0 : _colliderOffsetX, _collider.offset.y);
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