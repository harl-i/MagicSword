using System.Collections;
using UnityEngine;

public class CheckPlatformTransition : Transition
{
    [SerializeField] private RaycastDirection _raycastDirection;
    [SerializeField] private float _raycastLength;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _raycastFrequency;

    private bool _isColliderDetected;
    private float _raycastTimer;
    private float _offset = -0.2f;
    private RaycastHit2D _hit;

    private void Update()
    {
        StartCoroutine(DelayAndCheck(3f));
    }

    private IEnumerator DelayAndCheck(float delay)
    {
        yield return new WaitForSeconds(delay);

        _raycastTimer += Time.deltaTime;

        if (_raycastTimer >= _raycastFrequency)
        {
            Vector3 raycastOrigin = transform.position + Vector3.down * _offset;

            switch (_raycastDirection)
            {
                case RaycastDirection.Up:
                    _hit = Physics2D.Raycast(raycastOrigin, Vector2.up, _raycastLength, _layerMask);
                    break;
                case RaycastDirection.Down:
                    _hit = Physics2D.Raycast(raycastOrigin, Vector2.down, _raycastLength, _layerMask);
                    break;
                case RaycastDirection.Left:
                    _hit = Physics2D.Raycast(raycastOrigin, Vector2.left, _raycastLength, _layerMask);
                    break;
                case RaycastDirection.Right:
                    _hit = Physics2D.Raycast(raycastOrigin, Vector2.right, _raycastLength, _layerMask);
                    break;
                default:
                    break;
            }

            DrawRaycast(raycastOrigin, _raycastDirection);

            _raycastTimer = 0f;
        }
    }

    private void DrawRaycast(Vector3 raycastOrigin, RaycastDirection raycastDirection)
    {
        switch (raycastDirection)
        {
            case RaycastDirection.Up:
                Debug.DrawRay(raycastOrigin, Vector2.up * _raycastLength, _isColliderDetected ? Color.green : Color.red);
                break;
            case RaycastDirection.Down:
                Debug.DrawRay(raycastOrigin, Vector2.down * _raycastLength, _isColliderDetected ? Color.green : Color.red);
                break;
            case RaycastDirection.Left:
                Debug.DrawRay(raycastOrigin, Vector2.left * _raycastLength, _isColliderDetected ? Color.green : Color.red);
                break;
            case RaycastDirection.Right:
                Debug.DrawRay(raycastOrigin, Vector2.right * _raycastLength, _isColliderDetected ? Color.green : Color.red);
                break;
            default:
                break;
        }
    }
}
