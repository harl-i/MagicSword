using Enemies;
using UnityEngine;

namespace StateMachine
{
    public class BirdHitTransition : Transition
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CollisionTag collisionTag))
            {
                NeedTransit = true;
            }
        }
    }
}