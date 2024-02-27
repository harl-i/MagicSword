using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shield : MonoBehaviour 
{
    [SerializeField] private Player _player;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        transform.position = _player.transform.position;
        transform.rotation = _player.transform.rotation;

        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    private void Update()
    {
        transform.position = _player.transform.position;
        transform.rotation = _player.transform.rotation;
    }
}
