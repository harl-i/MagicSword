using System;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public int Coins { get; private set; }

    public static Action<int> CoinsChanged;

    public void IncreaseCoinsCount(int count)
    {
        Coins += count;
        CoinsChanged?.Invoke(Coins);
    }
}
