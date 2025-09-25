public class ResetTransition : Transition
{
    private void OnEnable()
    {
        ResetTrigger.OnResetTrtiggerHit += HandlePlayerHitTrigger;
    }

    private void OnDisable()
    {
        ResetTrigger.OnResetTrtiggerHit -= HandlePlayerHitTrigger;
        NeedTransit = false;
    }

    private void HandlePlayerHitTrigger()
    {
        NeedTransit = true;
    }
}
