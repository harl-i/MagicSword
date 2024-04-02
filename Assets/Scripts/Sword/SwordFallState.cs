using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))] 
public class SwordFallState : State
{
    private Rigidbody2D _rigidbody2D;
    private PolygonCollider2D _collider;
    public float fallSpeed = 1f;
    private bool isFalling = false;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        //_collider.enabled = false;

        //StartCoroutine(EnableCollider());
    }

    void Update()
    {
        if (!isFalling)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f);
            if (hit.collider != null)
            {
                // Найдена точка пересечения, начинаем падение
                isFalling = true;
                // Поворот меча к точке пересечения
                Vector2 direction = (hit.point - (Vector2)transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        else
        {
            // Падение меча вниз
            _rigidbody2D.velocity = new Vector2(0, -fallSpeed);
        }

        // Визуализация Raycast
        Debug.DrawRay(transform.position, Vector2.down * 100f, Color.red);
    }

    //private IEnumerator EnableCollider()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    _collider.enabled = true;
    //}
}
