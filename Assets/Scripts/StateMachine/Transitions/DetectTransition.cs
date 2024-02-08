using UnityEngine;

public class DetectTransition : Transition
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _playerLayer;

    private Transform _playerTransform;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);
        if (hit != null)
        {
            _playerTransform = hit.transform;

            _targetState.SetPlayerTransform(_playerTransform);
            NeedTransit = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
