using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EvasionState : State
{
    [SerializeField] [Range(0.5F, 2)] private float _arrowLength = 1.0F;

    [SerializeField] private float _rayLength;
    [SerializeField] private int _numberOfRays = 12;
    [SerializeField] private float _rayStartOffset = 0.1f;
    [SerializeField] private PolygonCollider2D _colliderForEnable;

    private Vector2 _newPosition;
    private bool _isMovingAway = false;
    private List<RaycastHit2D> _hits = new List<RaycastHit2D>();
    private RaycastHit2D _targetHit;
    private float _batSpeed = 1.5f;
    private float _safeDistance = 1.5f;
    private Vector2 _initialPosition;
    private float _delayForActivateCollider = 0.35f;

    private void OnEnable()
    {
        _initialPosition = transform.position;
        ThrowingRays();
    }

    private void Update()
    {
        if (_isMovingAway)
        {
            Vector2 moveDirection = _targetHit.point - _initialPosition;
            moveDirection.Normalize();
            _newPosition = _initialPosition + moveDirection * _safeDistance;

            transform.position = Vector3.Lerp(transform.position, _newPosition, _batSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _newPosition) <= 0.1f)
            {
                _isMovingAway = false;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (_isMovingAway)
        {
            Vector2 position = transform.position;
            var direction = _targetHit.point - position;

            if (direction.magnitude > 0.1f)
            {
                Handles.color = Color.red;
                Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(direction), _arrowLength, EventType.Repaint);
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_newPosition, 0.1f);
    }
#endif

    private void ThrowingRays()
    {
        _hits.Clear();

        _colliderForEnable.enabled = false;

        for (int i = 0; i < _numberOfRays; i++)
        {
            float angle = i * Mathf.PI * 2 / _numberOfRays;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 rayStartPoint = new Vector2(transform.position.x, transform.position.y) + direction * _rayStartOffset;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _rayLength);
            _hits.Add(hit);

            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + direction * _rayLength, Color.red);
        }

        RaycastHit2D clearHit = FindClearHit();

        _targetHit = clearHit;
        _isMovingAway = true;

        StartCoroutine(EnableColliderAfterDelay(_delayForActivateCollider));
    }

    private RaycastHit2D FindClearHit()
    {
        foreach (var hit in _hits)
        {
            if (hit.collider == null)
            {
                return hit;
            }
        }

        return _hits[0];
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _colliderForEnable.enabled = true;
    }
}
