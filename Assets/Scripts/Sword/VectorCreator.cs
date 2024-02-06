using System;
using UnityEngine;

public class VectorCreator : MonoBehaviour
{
    private Vector2 _initialMousePosition;
    private Vector2 _currentMousePosition;
    private float _vectorLength;

    public Action<Vector2> MouseDirectionChanged;
    public Action DirectionSelectionCompleted;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _currentMousePosition = Input.mousePosition;

            _vectorLength = Vector2.Distance(_currentMousePosition, _initialMousePosition);

            if (_vectorLength > 180f)
            {
                Vector2 direction = _currentMousePosition - _initialMousePosition;
                direction.Normalize();

                MouseDirectionChanged?.Invoke(direction);
            }
        }
        else if (Input.GetMouseButtonUp(0) && _vectorLength > 180f)
        {
            DirectionSelectionCompleted?.Invoke();
        }
    }
}
