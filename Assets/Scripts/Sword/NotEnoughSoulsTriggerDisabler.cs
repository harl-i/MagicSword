using UnityEngine;

public class NotEnoughSoulsTriggerDisabler : MonoBehaviour
{
    [SerializeField] private Portal _portal;

    private void OnEnable()
    {
        _portal.SoulsCollected += OnSoulsCollected;
    }

    private void OnDisable()
    {
        _portal.SoulsCollected -= OnSoulsCollected;
    }

    private void OnSoulsCollected()
    {
        gameObject.SetActive(false);
    }
}
