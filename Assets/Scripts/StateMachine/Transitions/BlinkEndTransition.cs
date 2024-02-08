using UnityEngine;

public class BlinkEndTransition : Transition
{
    public void AnimationEnded()
    {
        Debug.Log("NEED TRANSIT!!!!");
        NeedTransit = true;
    }
}
