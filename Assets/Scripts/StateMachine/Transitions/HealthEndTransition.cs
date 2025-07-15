using UnityEngine;

public class HealthEndTransition : Transition
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        if (_player.Health == 0)
        {
            NeedTransit = true;
        }
    }

    private void OnDisable()
    {
        NeedTransit = false;
    }
}
