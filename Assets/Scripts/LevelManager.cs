using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelEndScreen _levelEndScreen;

    private int _enemiesCount;

    public int Coins { get; private set; }
    public int EnemiesCount => _enemiesCount;

    public static Action<int> CoinsChanged;

    private void OnEnable()
    {
        Enemy.EnemyStarted += OnIncreaseEnemyCount;
        Enemy.EnemyDied += OnDecreaseEnemyCount;
        Player.CoinsIncrease += OnIncreaseCoins;
    }

    private void OnDisable()
    {
        Enemy.EnemyStarted -= OnIncreaseEnemyCount;
        Enemy.EnemyDied -= OnDecreaseEnemyCount;
        Player.CoinsIncrease -= OnIncreaseCoins;
    }

    private void OnIncreaseCoins(int count)
    {
        Coins += count;
        CoinsChanged?.Invoke(Coins);
    }

    private void OnIncreaseEnemyCount()
    {
        _enemiesCount++;
    }

    private void OnDecreaseEnemyCount()
    {
        _enemiesCount--;

        if (_enemiesCount == 0)
        {
            ShowLevelEndScreen();
        }
    }

    private void ShowLevelEndScreen()
    {
        _levelEndScreen.gameObject.SetActive(true);
    }
}
