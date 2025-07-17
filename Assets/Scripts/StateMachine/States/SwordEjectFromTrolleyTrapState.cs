using UnityEngine;

public class SwordEjectFromTrolleyTrapState : State
{
    [SerializeField] private float _ejectHeight = 1.5f;
    [SerializeField] private float _ejectSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 1000f;
    [SerializeField] private float _numberOfRotations = 1.5f;
    [SerializeField] private EndEjectTransition _endEjectTransition;

    private Vector3 _startPosition;
    private float _rotationProgress;
    private bool _isRotating = true;
    private float _targetHeight;

    private void OnEnable()
    {
        _rotationProgress = 0f;
        _isRotating = true;
        _startPosition = transform.position;
        _targetHeight = _startPosition.y + _ejectHeight;
    }

    private void Update()
    {
        if (_isRotating)
        {
            _rotationProgress += _rotationSpeed * Time.deltaTime;
            float rotationAngle = _rotationProgress;

            if (_rotationProgress >= 360f * _numberOfRotations)
            {
                rotationAngle = 360f * _numberOfRotations;
                _isRotating = false;
            }

            Vector3 newPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _targetHeight, transform.position.z), _ejectSpeed * Time.deltaTime);

            Quaternion newRotation = Quaternion.Euler(0, 0, rotationAngle);

            transform.position = newPosition;
            transform.rotation = newRotation;
        }
        else
        {
            _endEjectTransition.EjectEnded();
        }
    }
}
