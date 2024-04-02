using UnityEngine;

public class FallingRockCollisionTransition : Transition
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FaliingRock faliingRock))
        {
            NeedTransit = true;
            Debug.Log("Rock transition");
        }
    }
}
