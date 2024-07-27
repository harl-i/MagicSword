using System;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public static Action<Soul> SoulSpawned;

    private void OnEnable()
    {
        SoulSpawned?.Invoke(this);
    }
}
