using UnityEngine;

public class PlatformUnderEnemyTransition : Transition
{
    [SerializeField] private float _raycastLength;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _raycastFrequency;

    private bool _isColliderDetected;
    private float _raycastTimer;
    private float _offset = -0.2f;

    private void Update()
    {
        _raycastTimer += Time.deltaTime;

        if (_raycastTimer >= _raycastFrequency)
        {
            Vector3 raycastOrigin = transform.position + Vector3.down * _offset;

            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, _raycastLength, _layerMask);

            _isColliderDetected = hit.collider != null;
            if (!_isColliderDetected)
            {
                NeedTransit = true;
            }

            Debug.DrawRay(raycastOrigin, Vector2.down * _raycastLength, _isColliderDetected ? Color.green : Color.red);

            _raycastTimer = 0f;
        }
    }
}
