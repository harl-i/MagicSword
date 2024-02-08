using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] protected State _targetState;

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
