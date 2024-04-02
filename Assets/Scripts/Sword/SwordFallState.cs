using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class SwordFallState : State
{
    private Rigidbody2D _rigidbody2D;
    private float _spinForce = 0.00000000001f;
    private float _division—oefficient = 100f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        _rigidbody2D.AddTorque(_spinForce / _division—oefficient, ForceMode2D.Impulse);
    }
}