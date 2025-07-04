public class ExplosionEndTransition : Transition
{
    public void ExplosionEnded()
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        NeedTransit = false;
    }
}
