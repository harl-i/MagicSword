using UnityEngine;

public class ExitSnowdriftTransition : Transition
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ExitSnowdriftTrigger exitSnowdriftTrigger))
        {
            NeedTransit = true;
        }
    }
}
