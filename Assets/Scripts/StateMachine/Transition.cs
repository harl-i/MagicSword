using UnityEngine;

namespace StateMachine
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] protected State TargetState;

        public State TargetStateProperty => TargetState;
        public bool NeedTransit { get; protected set; }

        private void OnEnable() => NeedTransit = false;
    }
}
