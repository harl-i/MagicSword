using System;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetX;

    public static Action<Soul> SoulSpawned;

    private void OnEnable()
    {
        SoulSpawned?.Invoke(this);
        SetEnablePosition();
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
        transform.position = new Vector2(_enemyTransform.position.x + _offsetX, 
            _enemyTransform.position.y + _offsetY);
    }
}
