using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _coins = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _coins.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MagicSword magicSword))
        {
            foreach (var coin in _coins)
            {
                coin.SetActive(true);
            }
        }
    }
}
