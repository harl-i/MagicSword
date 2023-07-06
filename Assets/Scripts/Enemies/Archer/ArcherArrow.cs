using UnityEngine;

public class ArcherArrow : MonoBehaviour, IObjectFromPool
{
    [SerializeField] private float _speed;
    [SerializeField] private ArrowDirection _arrowDirection;

    private Transform _parent;

    private void Awake()
    {
        _parent = transform.parent.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            ReturnToPool();
        }
    }

    private void Update()
    {
        if (_arrowDirection == ArrowDirection.left)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        else if (_arrowDirection == ArrowDirection.right)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ReturnToPool()
    {
        if (transform.parent == null)
        {
            transform.SetParent(_parent.transform);
        }

        gameObject.SetActive(false);
    }
}

public enum ArrowDirection
{
    left,
    right
}
