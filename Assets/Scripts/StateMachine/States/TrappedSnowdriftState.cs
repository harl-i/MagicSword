using UnityEngine;

[RequireComponent(typeof(VectorCreator))]
public class TrappedSnowdriftState : State
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private VectorCreator _vectorCreator;
    private Vector3 _direction;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private bool _isCaughtInTrap;

    private void Awake()
    {
        _vectorCreator = GetComponent<VectorCreator>();
        _isCaughtInTrap = true;
    }

    private void OnEnable()
    {
        _isCaughtInTrap = true;
        UpdateTargetPosition();

        _vectorCreator.enabled = true;

        _vectorCreator.MouseDirectionChanged += HandleMouseDirectionChange;
        _vectorCreator.DirectionSelectionCompleted += HandleDirectionSelectionCompleted;
    }

    private void OnDisable()
    {
        _vectorCreator.MouseDirectionChanged -= HandleMouseDirectionChange;
        _vectorCreator.DirectionSelectionCompleted -= HandleDirectionSelectionCompleted;

        _vectorCreator.enabled = false;
    }

    private void Update()
    {
        _vectorCreator.enabled = true;

        if (_isCaughtInTrap == false)
        {
            MoveBackward();
        }
    }

    private void HandleMouseDirectionChange(Vector2 direction)
    {
        _direction = direction;
        ShowArrow();
    }

    private void HandleDirectionSelectionCompleted()
    {
        _vectorCreator.enabled = false;

        _arrow.gameObject.SetActive(false);

        if (_isCaughtInTrap)
            _isCaughtInTrap = false;

        UpdateTargetPosition();
    }

    private void ShowArrow()
    {
        _arrow.gameObject.SetActive(true);
        _arrow.gameObject.transform.position = transform.position;
        _arrow.gameObject.transform.up = _direction;
    }

    private void UpdateTargetPosition()
    {
        _startPosition = gameObject.transform.position;
        Vector2 localDownDirection = transform.up;

        _targetPosition = _startPosition - localDownDirection * _distance;
    }

    private void MoveBackward()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_targetPosition, 0.1f);
    }
}
