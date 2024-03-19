using UnityEngine;

public class HideTurretState : State
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _moveSpeed = 1f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isMoving;
    private float _blockDistance = 0.09f;

    private void OnEnable()
    {
        _isMoving = true;
        _startPosition = transform.position;
        _targetPosition = _startPosition + new Vector3(_moveDistance, 0, 0);
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) < _blockDistance)
            {
                _isMoving = false;
            }
        }
    }
}
