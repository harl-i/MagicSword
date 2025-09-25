using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TrapActivationState : State
{
    [SerializeField] private float _raycastLength = 5f;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Transform _raycastOrigin;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Activation");
        _boxCollider2D.enabled = false;
    }

    private void OnDisable()
    {
        _polygonCollider2D.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 raycastOrigin = _raycastOrigin.position;
        Vector3 raycastDirection = transform.right;
        Gizmos.DrawRay(raycastOrigin, raycastDirection * _raycastLength);
    }

    public void CheckPlayerInTrap()
    {
        _polygonCollider2D.enabled = true;
        _boxCollider2D.enabled = false;
    }
}
