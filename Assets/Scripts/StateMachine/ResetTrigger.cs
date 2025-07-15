using System;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    public static Action OnResetTrtiggerHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            OnResetTrtiggerHit?.Invoke();
        }
    }
}