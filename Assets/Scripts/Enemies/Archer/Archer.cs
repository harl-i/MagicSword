using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Archer : MonoBehaviour, IAttackable
{
    [SerializeField] private ArcherArrowsPool _pool;
    private Animator _animator;

    private const string IsDetect = "isDetect";
    private const string IsDie = "isDie";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MagicSword magicSword))
        {
            _animator.SetBool(IsDie, true);
        }
    }

    public void StartAttack()
    {
        _animator.SetBool(IsDetect, true);
    }

    private void Shoot()
    {
        if (_pool != null)
        {

            _pool.TryGetObject(out GameObject bullet);

            if (bullet != null)
            {
                bullet.transform.position = _pool.transform.position;
                if (bullet.transform.parent != null)
                {
                    bullet.transform.SetParent(null);

                }
                bullet.SetActive(true);
            }
            else
            {
                Debug.Log("bullets pool is empty");
            }
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
