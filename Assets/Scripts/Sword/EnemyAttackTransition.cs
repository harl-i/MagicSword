using UnityEngine;

[RequireComponent(typeof(SwordMovingState))]
public class EnemyAttackTransition : Transition
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_player.IsLaunched && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            NeedTransit = true;
        }

        if (collision.gameObject.TryGetComponent(out Thorn thorn))
        {
            NeedTransit = true;
        }
    }
}
