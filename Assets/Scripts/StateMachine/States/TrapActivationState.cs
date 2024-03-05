using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrapActivationState : State
{
    [SerializeField] private float _raycastLength = 5f;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Transform _raycastOrigin;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger("Activation");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 raycastOrigin = _raycastOrigin.position;
        Vector3 raycastDirection = transform.forward;
        Gizmos.DrawRay(raycastOrigin, raycastDirection * _raycastLength);
    }

    public void CheckPlayerInTrap()
    {
        //RaycastHit hit;
        //if (Physics2D.Raycast(_raycastOrigin.position, transform.forward, out hit, _raycastLength, _playerLayerMask))
        //{
        //    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        //    {
        //        Player player = hit.collider.gameObject.GetComponent<Player>();
        //        ApplyDamage(player);
        //    }
        //}

        RaycastHit2D hit = Physics2D.Raycast(_raycastOrigin.position, Vector2.right, _raycastLength, _playerLayerMask);

        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();
            ApplyDamage(player);
        }
    }

    private void ApplyDamage(Player player)
    {
        player.TakeDamage();
    }
}
