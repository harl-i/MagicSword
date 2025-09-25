using Obstacles;
using StateMachine;
using UnityEngine;

namespace Sword
{
    public class FallingRockCollisionTransition : Transition
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out FaliingRock faliingRock))
            {
                NeedTransit = true;
            }
        }
    }
}