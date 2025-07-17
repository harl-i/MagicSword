using UnityEngine;

public class Arrow : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FillWithRed()
    {
        _spriteRenderer.color = Color.red;
    }

    public void RemoveRedFill()
    {
        _spriteRenderer.color = Color.white;
    }
}
