using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PatrolState : State
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    
    private int waypointIndex = 0;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
 
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        _spriteRenderer.flipX = direction.x < 0;
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