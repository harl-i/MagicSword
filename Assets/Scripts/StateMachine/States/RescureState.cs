using UnityEngine;

[RequireComponent(typeof(VectorCreator))]
public class RescureState : State
{
    [SerializeField] private float _distance;

    private VectorCreator _vectorCreator;
    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private Vector3 _direction;

    private void OnEnable()
    {
        UpdateTargetPosition();
        MoveBackward();
    }

    private void UpdateTargetPosition()
    {
        _startPosition = gameObject.transform.position;
        Vector2 localDownDirection = transform.up;

        _targetPosition = _startPosition - localDownDirection * _distance;
    }

    private void MoveBackward()
    {
        //transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, _targetPosition, 0.1f * Time.deltaTime);
        transform.position = new Vector2(_targetPosition.x, _targetPosition.y);
    }
}
