using UnityEngine;

namespace StateMachine
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] protected State StateTarget;

        public State TargetState => StateTarget;
        public bool NeedTransit { get; protected set; }

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}