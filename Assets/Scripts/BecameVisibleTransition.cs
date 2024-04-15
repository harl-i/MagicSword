using UnityEngine;

public class BecameVisibleTransition : Transition
{
    private void OnBecameVisible()
    {
        NeedTransit = true;
    }
}
