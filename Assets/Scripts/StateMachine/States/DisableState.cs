public class DisableState : State
{
    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
}
