using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class TrappedTrolleyState : State
{
    [SerializeField] private float _offsetY = 3f;

    private PolygonCollider2D _swordCollider;
    private Transform _trolleyTrapTransform;
    private PolygonCollider2D _trapMainCollider;
    private void Awake()
    {
        _swordCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TrollleyTrap trollleyTrap))
        {
            Debug.Log("Trolley trap collision");
            _trolleyTrapTransform = trollleyTrap.gameObject.transform;

            _trapMainCollider = trollleyTrap.GetMainTrolleyCollider();

            if (_trapMainCollider != null)
            {
                Physics2D.IgnoreCollision(_swordCollider, _trapMainCollider);
            }
        }
    }

    private void Update()
    {
        if (_trolleyTrapTransform != null)
        {
            Vector2 positionInTrap = new Vector2(_trolleyTrapTransform.position.x, _trolleyTrapTransform.position.y + _offsetY);
            transform.position = positionInTrap;
        }
    }

    private void OnDisable()
    {
        _trolleyTrapTransform = null;

        if (_trapMainCollider != null)
        {
            Physics2D.IgnoreCollision(_swordCollider, _trapMainCollider, false);
        }
    }
}
