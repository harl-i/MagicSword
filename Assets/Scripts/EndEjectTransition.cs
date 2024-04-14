public class EndEjectTransition : Transition
{
    private void OnDisable()
    {
        NeedTransit = false;
    }

    public void EjectEnded()
    {
        NeedTransit = true;
    }
}
