using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallingRocksSpawner : MonoBehaviour
{
    [SerializeField] private FaliingRock _rockPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;
    [SerializeField] private float _delay;

    protected List<FaliingRock> _pool = new List<FaliingRock>();
    private float _timer;

    private void Start()
    {
        Initialize(_rockPrefab);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _delay)
        {
            _timer = 0f;
            SpawnRock();
        }
    }

    private void Initialize(FaliingRock prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            FaliingRock spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    private void SpawnRock()
    {
        TryGetObject(out FaliingRock faliingRock);
        faliingRock.transform.position = _container.position;
        faliingRock.gameObject.SetActive(true);
    }

    private bool TryGetObject(out FaliingRock result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
