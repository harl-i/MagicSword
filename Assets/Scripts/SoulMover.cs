using System;
using System.Collections;
using UnityEngine;

public class SoulMover : MonoBehaviour
{
    [SerializeField] private Transform _portal;

    private Soul _soul;

    private void OnEnable()
    {
        Soul.SoulSpawned += HandleSoulSpawned;
    }

    private void OnDisable()
    {
        Soul.SoulSpawned -= HandleSoulSpawned;
    }

    private void HandleSoulSpawned(Soul soul)
    {
        _soul = soul;
    }

    private void Update()
    {
        if (_soul != null)
        {
            StartCoroutine(MoveSoulToPortal(_soul));
        }
    }

    private IEnumerator MoveSoulToPortal(Soul soul)
    {
        float speed = 0.01f; 
        Vector3 direction = (_portal.position - soul.transform.position).normalized; 
        float distance = Vector3.Distance(soul.transform.position, _portal.position); 

        while (distance > 0.1f) 
        {
            //direction.Normalize();

            soul.transform.position += direction * speed * Time.deltaTime; 
            distance = Vector3.Distance(soul.transform.position, _portal.position);
            yield return null;
        }
    }
}
