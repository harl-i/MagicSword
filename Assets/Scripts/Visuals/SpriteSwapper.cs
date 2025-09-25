using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private Sprite _targetSprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteRenderer.sprite = _targetSprite;
    }
}
