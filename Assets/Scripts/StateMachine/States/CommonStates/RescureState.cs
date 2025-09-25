using UnityEngine;

[RequireComponent(typeof(VectorCreator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class RescureState : State
{
    [SerializeField] private float _distance;

    private PolygonCollider2D _playerCollider;
    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private void Awake()
    {
        _playerCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        _playerCollider.enabled = false;
        UpdateTargetPosition();
        MoveBackward();
    }

    private void OnDisable()
    {
        _playerCollider.enabled = true;
    }

    private void UpdateTargetPosition()
    {
        _startPosition = gameObject.transform.position;
        Vector2 localDownDirection = transform.up;

        _targetPosition = _startPosition - localDownDirection * _distance;
    }

    private void MoveBackward()
    {
        transform.position = new Vector2(_targetPosition.x, _targetPosition.y);
    }
}
