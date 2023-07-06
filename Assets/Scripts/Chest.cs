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
}
