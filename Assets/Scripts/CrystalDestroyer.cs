using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CrystalDestroyer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Destroy()
    {
        _animator.SetTrigger("Destroy");
    }
}