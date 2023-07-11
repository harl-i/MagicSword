using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _enemiesCount;

    private void OnEnable()
    {
        Enemy.OnEnemyStarted += IncreaseEnemyCount;
        Enemy.OnEnemyDied += DecreaseEnemyCount;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyStarted -= IncreaseEnemyCount;
        Enemy.OnEnemyDied -= DecreaseEnemyCount;
    }

    private void IncreaseEnemyCount()
    {
        _enemiesCount++;
        Debug.Log(_enemiesCount);
    }

    private void DecreaseEnemyCount()
    {
        _enemiesCount--;
        Debug.Log(_enemiesCount);
    }
}
