using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PursuitState : State
{
    private SpriteRenderer _spriteRenderer;

    public float _speed = 2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PursueTarget();
    }

    private void PursueTarget()
    {
        Vector2 direction = (Player.position - transform.position).normalized;
        transform.position += (Vector3)direction * _speed * Time.deltaTime;

        _spriteRenderer.flipX = direction.x < 0;
    }
}
