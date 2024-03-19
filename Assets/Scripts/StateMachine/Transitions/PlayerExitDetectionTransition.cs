using UnityEngine;

public class PlayerExitDetectionTransition : Transition
{
    [SerializeField] private DetectionZoneType _detectionZoneType;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private Vector2 _detectionRectangleSize = new Vector2(10f, 5f);
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _offsetY;

    private bool _playerInside;
    private float _checkInterval = 1f;
    private float _timeSinceLastCheck = 0f;

    private void Update()
    {
        _timeSinceLastCheck += Time.deltaTime;

        if (_timeSinceLastCheck > _checkInterval)
        {
            _timeSinceLastCheck = 0f;

            if (_detectionZoneType == DetectionZoneType.Circle)
            {
                _playerInside = IsPlayerInsideCircle();
            }
            else if (_detectionZoneType == DetectionZoneType.Rectangle)
            {
                _playerInside = IsPlayerInsideRectangle();
            }

            if (!_playerInside)
            {
                HandlePlayerExit();
            }
        }
    }

    private bool IsPlayerInsideCircle()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);
        return hit != null;
    }

    private bool IsPlayerInsideRectangle()
    {
        Vector2 offset = new Vector2(0, _offsetY);
        Vector2 adjustedPosition = (Vector2)transform.position + offset;

        Collider2D hit = Physics2D.OverlapBox(adjustedPosition, _detectionRectangleSize, 0f, _playerLayer);
        return hit != null;
    }

    private void HandlePlayerExit()
    {
        _playerInside = false;

        NeedTransit = true;
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
            Vector2 offset = new Vector2(0, _offsetY);
            Vector2 adjustedPosition = (Vector2)transform.position + offset;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(adjustedPosition, _detectionRectangleSize);
        }
    }
}
