using UnityEngine;

public class TrappedInSnowdriftTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Snowdrift snowdrift))
        {
            NeedTransit = true;
        }
    }
}
