using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour, IObjectFromPool
{
    [SerializeField] private CoinType _coinType;
    [SerializeField] private LayerMask _coinLayer;
    [SerializeField] private float _colliderEnableDelay;
    [SerializeField] private float _moveDelay;
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _moveDurationToTarget;

    private Transform _target;
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;
    private Transform _parent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _parent = transform.parent.transform;
        _circleCollider.enabled = false;
    }

    private void OnEnable()
    {
        Invoke(nameof(EnableCollider), _colliderEnableDelay);

        if (_coinType == CoinType.GameCoin)
        {
            _rigidbody.AddForce(GetRandomDirection() * GetRandomForce(), ForceMode2D.Impulse);
        }
        StartCoroutine(MoveCoinToTarget());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var normal = other.contacts[0].normal;
        var direction = Vector2.Reflect(_rigidbody.velocity.normalized, normal);

        _rigidbody.velocity = direction * GetRandomForce();
    }

    private Vector2 GetRandomDirection()
    {
        return Quaternion.Euler(0, 0, Random.Range(_minAngle, _maxAngle)) * Vector2.up;
    }

    private float GetRandomForce()
    {
        return Random.Range(_minForce, _maxForce);
    }

    private void EnableCollider()
    {
        _circleCollider.enabled = true;
    }

    private IEnumerator MoveCoinToTarget()
    {
        yield return new WaitForSeconds(_moveDelay);

        float elapsedTime = 0.0f;
        Vector3 startingPos = transform.position;
        Vector3 targetPos = _target.position;

        while (elapsedTime < _moveDurationToTarget)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / _moveDurationToTarget);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void ReturnToPool()
    {
        if (transform.parent == null)
        {
            transform.SetParent(_parent.transform);
        }

        gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

public enum CoinType
{
    GameCoin,
    UICoin
}
