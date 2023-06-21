using UnityEngine;

public class ArcherArrow : MonoBehaviour, IObjectFromPool
{
    [SerializeField] private float _speed;
    
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
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
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
