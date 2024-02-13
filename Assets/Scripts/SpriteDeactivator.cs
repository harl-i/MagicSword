using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDeactivator : MonoBehaviour
{
    [SerializeField] private SpriteSwapperActivator _spriteSwapperActivator;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteSwapperActivator.AllSpritesSwapped += HandleAllSpriteSwap;
    }

    private void OnDisable()
    {
        _spriteSwapperActivator.AllSpritesSwapped -= HandleAllSpriteSwap;
    }

    private void HandleAllSpriteSwap()
    {
        _spriteRenderer.enabled = false;
    }
}
