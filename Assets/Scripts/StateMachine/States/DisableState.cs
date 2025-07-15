using UnityEngine;

public class DisableState : State
{
    [SerializeField] private Soul _soul;

    private void OnEnable()
    {
        if (_soul != null)
        {
            _soul.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
