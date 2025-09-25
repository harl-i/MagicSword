using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TapTip : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
