using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeAttackState : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator.SetTrigger("Attack");
    }
}
