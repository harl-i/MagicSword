using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RandomCrystalGrow : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private float _levelCenter = 0f;

    private Vector2 _rightGrowColliderOffset = new Vector2(-1.137541f, 0.09084988f);
    private Vector2 _leftGrowColliderOffset = new Vector2(1.15f, 0.09084988f);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    private void OnEnable()
    {
        ActivateRandomGrowTrigger();
        SetGrowDirection();
        ColliderOffset();
    }

    private void ActivateRandomGrowTrigger()
    {
        float random = Random.Range(0f, 1f);

        if (random < 0.5f)
        {
            _animator.SetTrigger("RedCrystalGrow");
        }
        else
        {
            _animator.SetTrigger("BlueCrystalGrow");
        }
    }

    private void SetGrowDirection()
    {
        if (transform.position.x > _levelCenter)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void ColliderOffset()
    {
        if (transform.position.x > _levelCenter)
        {
            _collider.offset = _rightGrowColliderOffset;
        }
        else
        {
            _collider.offset = _leftGrowColliderOffset;
        }
    }
}
