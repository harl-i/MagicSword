namespace StateMachine
{
    public class AnimationEndTransition : Transition
    {
        public void AnimationEnded()
        {
            NeedTransit = true;
        }
    }
}