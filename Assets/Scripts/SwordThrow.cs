using UnityEngine;
using UnityEngine.Events;

public class SwordThrow : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private float _angle;
    private bool _canMove;
    private bool _isFirstThrow;

    public UnityAction<bool> StuckInWall;

    private void Awake()
    {
        _canMove = false;
        _isFirstThrow = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _canMove = false;
        }
    }

    private void Update()
    {
        if (_canMove)
        {
            Throw();
        }
    }

    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            if (_isFirstThrow)
            {
                StuckInWall?.Invoke(false);
            }
            else
            {
                StuckInWall?.Invoke(true);
            }

            CalculateDirection();
            CalculateAngle();
        }
    }

    private void OnMouseUp()
    {
        _canMove = true;
        _isFirstThrow = false;
        StuckInWall?.Invoke(false);
    }

    private void CalculateDirection()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _direction.z = 0f;
        _direction.Normalize();

        Debug.DrawRay(transform.position, _direction);
    }

    private void CalculateAngle()
    {
        _angle = Vector2.SignedAngle(transform.InverseTransformDirection(transform.up), _direction);

        Debug.DrawRay(transform.localPosition, _direction, Color.red);
    }

    private void Throw()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, transform.localRotation.z + _angle);
        transform.Translate(transform.InverseTransformDirection(transform.up) * Time.deltaTime * _speed);
    }
}
