using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NotPhysicalFallState : State
{
    [SerializeField] private float _fallSpeed = 10f;
    [SerializeField] private float _gravity = 9.81f;

    private Vector3 _velocity;
    private Animator _animator;
    private float _delay = 0.5f;
    private bool _canDetectCollision;
    private bool _isFall;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _canDetectCollision = false;
        _isFall = true;
        _velocity = new Vector3(0, -_fallSpeed, 0);
        _animator.SetTrigger("StartFall");

        StartCoroutine(DelayBeforeDetectCollision(_delay));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_canDetectCollision)
        {
            if (collision.collider.TryGetComponent(out Obstacle obstacle))
            {
                _animator.SetTrigger("EndFall");
                _isFall = false;
            }
        }
    }

    private void Update()
    {
        if (_isFall)
        {
            _velocity.y -= _gravity * Time.deltaTime;

            transform.position += _velocity * Time.deltaTime;
        }
    }

    private IEnumerator DelayBeforeDetectCollision(float delay)
    {
        yield return new WaitForSeconds(delay);

        _canDetectCollision = true;
    }
}
