using UnityEngine;

public class RescureTransition : Transition
{
    [SerializeField] private BoxCollider2D _rescureTriggerCollider;

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider == _rescureTriggerCollider || contact.otherCollider == _rescureTriggerCollider)
            {
                NeedTransit = true;
                return;
            }
        }
    }

}
