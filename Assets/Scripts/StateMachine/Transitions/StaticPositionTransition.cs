using UnityEngine;

public class StaticPositionTransition : Transition
{
    [SerializeField] private float _timeout = 2f;

    private Vector3 _lastPosition;
    private float _timer;

    private void Start()
    {
        _lastPosition = transform.position;
        _timer = _timeout;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (transform.position == _lastPosition)
            {
                NeedTransit = true;
            }
            else
            {
                _lastPosition = transform.position;
                _timer = _timeout;
            }
        }
        else
        {
            _timer = _timeout;
        }
    }
}
