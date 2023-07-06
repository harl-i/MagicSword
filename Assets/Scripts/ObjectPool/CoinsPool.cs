using UnityEngine;

public class CoinsPool : ObjectPool
{
    [SerializeField] private Coin _coin;

    private void Awake()
    {
        Initialize(_coin);
    }
}
