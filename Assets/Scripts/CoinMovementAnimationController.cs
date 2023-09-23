using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovementAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string RepeatCount = "repeatCount";

    private int _repeatCount;

    private void OnEnable()
    {
        StartCoinAnimation();
    }

    private void OnAnimationIteration()
    {
        _repeatCount--;
        _animator.SetInteger(RepeatCount, _repeatCount);
    }

    public void SetRepeatCount(int repeatCount)
    {
        _repeatCount = repeatCount;
    }

    private void StartCoinAnimation()
    {
        _animator.SetInteger(RepeatCount, _repeatCount);
    }
}
