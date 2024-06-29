using UnityEngine;

[RequireComponent(typeof(SwordMovingState))]
public class EnemyAttackTransition : Transition
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_player.IsLaunched && !_player.IsShieldActivated && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            NeedTransit = true;
        }

        if (!_player.IsShieldActivated && collision.gameObject.TryGetComponent(out Thorn thorn))
        {
            NeedTransit = true;
        }
    }

    public void Transition()
    {
        NeedTransit = true;     
    }
}
