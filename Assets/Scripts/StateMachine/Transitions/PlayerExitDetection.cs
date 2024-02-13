using UnityEngine;

public class PlayerExitDetection : Transition
{
    [SerializeField] private DetectionZoneType _detectionZoneType;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private Vector2 _detectionRectangleSize = new Vector2(10f, 5f);
    [SerializeField] private LayerMask _playerLayer;

    private bool _playerInside;
    private float _checkInterval = 1f;
    private float _timeSinceLastCheck = 0f;

    private void Update()
    {
        Debug.Log("Player Inside: " + _playerInside);
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
        Vector2 halfSize = _detectionRectangleSize / 2;
        Collider2D hit = Physics2D.OverlapBox(transform.position, _detectionRectangleSize, 0f, _playerLayer);
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
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _detectionRectangleSize);
        }
    }
}
