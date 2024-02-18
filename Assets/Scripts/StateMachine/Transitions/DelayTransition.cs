using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTransition : Transition
{
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        StartCoroutine(Delay(_delay));
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        NeedTransit = true;
    }
}
