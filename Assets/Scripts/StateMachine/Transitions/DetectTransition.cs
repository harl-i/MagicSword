using UnityEngine;

public class DetectTransition : Transition
{
    [SerializeField] private DetectionZoneType _detectionZoneType;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private Vector2 _detectionRectangleSize = new Vector2(10f, 5f);
    [SerializeField] private LayerMask _playerLayer;

    private Transform _playerTransform;

    private void Update()
    {
        if (_detectionZoneType == DetectionZoneType.Circle)
        {
            DetectUsingCircle();
        }
        else if (_detectionZoneType == DetectionZoneType.Rectangle)
        {
            DetectUsingRectangle();
        }
    }

    private void DetectUsingCircle()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);
        if (hit != null)
        {
            _playerTransform = hit.transform;

            _targetState.SetPlayerTransform(_playerTransform);
            NeedTransit = true;
        }
    }

    private void DetectUsingRectangle()
    {
        Vector2 halfSize = _detectionRectangleSize / 2;
        Collider2D hit = Physics2D.OverlapBox(transform.position, _detectionRectangleSize, 0f, _playerLayer);
        if (hit != null)
        {
            _playerTransform = hit.transform;

            _targetState.SetPlayerTransform(_playerTransform);
            NeedTransit = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_detectionZoneType == DetectionZoneType.Circle)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
        else if (_detectionZoneType == DetectionZoneType.Rectangle)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _detectionRectangleSize);
        }
    }
}

public enum DetectionZoneType
{
    Circle,
    Rectangle
}
