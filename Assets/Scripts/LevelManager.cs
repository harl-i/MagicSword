using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelEndScreen _levelEndScreen;

    private int _enemiesCount;

    public int EnemiesCount => _enemiesCount;

    private void OnEnable()
    {
        Enemy.EnemyStarted += IncreaseEnemyCount;
        Enemy.EnemyDied += DecreaseEnemyCount;
    }

    private void OnDisable()
    {
        Enemy.EnemyStarted -= IncreaseEnemyCount;
        Enemy.EnemyDied -= DecreaseEnemyCount;
    }

    private void IncreaseEnemyCount()
    {
        _enemiesCount++;
        Debug.Log(_enemiesCount);
    }

    private void DecreaseEnemyCount()
    {
        _enemiesCount--;

        if (_enemiesCount == 0)
        {
            ShowLevelEndScreen();
        }

        Debug.Log(_enemiesCount);
    }

    private void ShowLevelEndScreen()
    {
        _levelEndScreen.gameObject.SetActive(true);
    }
}
