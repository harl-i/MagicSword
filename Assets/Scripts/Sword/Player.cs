using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action<int> CoinsIncrease;

    public void IncreaseCoinsCount(int count)
    {
        CoinsIncrease?.Invoke(count);
    }
}
