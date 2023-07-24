using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCountDisplay : MonoBehaviour
{
    [SerializeField] private Scull _scullTemplate;

    private int _enemiesCount;
    private List<Scull> _sculls = new List<Scull>();

    public int EnemiesCount => _enemiesCount;

    private void OnEnable()
    {
        Enemy.EnemyStarted += OnIncreaseEnemyCount;
        Enemy.EnemyDied += OnDecreaseEnemyCount;
    }

    private void OnDisable()
    {
        Enemy.EnemyStarted -= OnIncreaseEnemyCount;
        Enemy.EnemyDied -= OnDecreaseEnemyCount;
    }

    private void OnIncreaseEnemyCount()
    {
        _enemiesCount++;
        CreateScull();
        Debug.Log(_enemiesCount);
    }

    private void OnDecreaseEnemyCount()
    {
        _enemiesCount--;

        Scull scull = _sculls.FirstOrDefault(skull => !skull.IsDead);
        scull.EnemyDied();

        Debug.Log(_enemiesCount);
    }

    private void CreateScull()
    {
        Scull newScull = Instantiate(_scullTemplate, gameObject.transform);
        _sculls.Add(newScull);
    }
}
