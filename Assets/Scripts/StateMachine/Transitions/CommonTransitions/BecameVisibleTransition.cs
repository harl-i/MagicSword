namespace StateMachine
{
    public class BecameVisibleTransition : Transition
    {
        private void OnBecameVisible()
        {
            NeedTransit = true;
        }
    }
}