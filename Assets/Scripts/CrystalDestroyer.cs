using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class CrystalDestroyer : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Destroy()
    {
        _animator.SetTrigger("CrystalDestroy");
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}