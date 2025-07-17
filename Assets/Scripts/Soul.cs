using System;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetX;

    public static Action<Soul> OnSoulSpawned;
    public Action OnSoulDisabled;

    private void OnEnable()
    {
        OnSoulSpawned?.Invoke(this);
        SetEnablePosition();
    }

    private void OnDisable()
    {
        OnSoulDisabled?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Portal portal))
        {
            gameObject.SetActive(false);
        }
    }

    private void SetEnablePosition()
    {
        transform.position = new Vector2(_enemyTransform.position.x + _offsetX, _enemyTransform.position.y + _offsetY);
    }
}
