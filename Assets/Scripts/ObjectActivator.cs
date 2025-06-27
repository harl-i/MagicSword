using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject _objectForActivate;

    private void OnEnable()
    {
        _objectForActivate.SetActive(true);
    }
}
