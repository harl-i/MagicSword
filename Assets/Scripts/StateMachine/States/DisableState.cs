using UnityEngine;

public class DisableState : State
{
    [SerializeField] private Soul _soul;

    private float _delay = 5f;

    private void OnEnable()
    {
        if (_soul != null)
        {
            _soul.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
