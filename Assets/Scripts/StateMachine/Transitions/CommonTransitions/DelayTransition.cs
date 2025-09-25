using System.Collections;
using UnityEngine;

namespace StateMachine
{
    public class DelayTransition : Transition
    {
        [SerializeField] private float _delay;

        private void OnEnable()
        {
            NeedTransit = false;
            StartCoroutine(Delay(_delay));
        }

        private void OnDisable()
        {
            NeedTransit = false;
        }

        private IEnumerator Delay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);

            NeedTransit = true;
        }
    }
}