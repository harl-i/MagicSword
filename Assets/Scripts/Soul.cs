using System;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public static Action<Soul> SoulSpawned;

    private void OnEnable()
    {
        SoulSpawned?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Portal portal))
        {
            gameObject.SetActive(false);
        }
    }
}
