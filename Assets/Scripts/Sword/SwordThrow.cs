using UnityEngine;
using UnityEngine.Events;

public class SwordThrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Arrow _arrow;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _raycastDistance;

    private Vector3 _direction;
    private float _angle;
    private float _throwDistance = 1f;
    private bool _canMove;
    private bool _isFirstThrow;


    public UnityAction<bool> StuckInWall;

    private void Start()
    {
        _canMove = false;
        _isFirstThrow = true;
        _arrow.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _canMove = false;
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
            ShowArrow();
        }
    }

    private void OnMouseUp()
    {
        if (CheckThrowDistance() && CheckPlatformInMovementDirection())
        {
            _canMove = true;
            _isFirstThrow = false;
        }
        StuckInWall?.Invoke(false);
        _arrow.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_canMove)
        {
            Throw();
        }
    }

    private void ShowArrow()
    {
        _arrow.gameObject.SetActive(true);
        _arrow.gameObject.transform.position = transform.position;
        _arrow.gameObject.transform.up = _direction;
    }

    private void CalculateDirection()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _direction.z = 0f;
        _direction.Normalize();
    }

    private void CalculateAngle()
    {
        _angle = Vector2.SignedAngle(transform.InverseTransformDirection(transform.up), _direction);
    }

    private void Throw()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, transform.localRotation.z + _angle);
        transform.Translate(transform.InverseTransformDirection(transform.up) * Time.deltaTime * _speed);
    }

    private bool CheckThrowDistance()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, transform.position);

        if (distance > _throwDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckPlatformInMovementDirection()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, _direction, _raycastDistance, _obstacleLayer);

        Debug.DrawLine(transform.position, transform.position + _direction * _raycastDistance, Color.cyan);
        return !raycastHit;
    }
}
