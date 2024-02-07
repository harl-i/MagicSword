public class BlinkEndTransition : Transition
{
    public void AnimationEnded()
    {
        NeedTransit = true;
    }
}
