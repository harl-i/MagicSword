using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(VectorCreator))]
public class SwordThrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Arrow _arrow;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private Transform _bladeEnd;

    private VectorCreator _vectorCreator;
    private Vector3 _direction;
    private float _angle;
    private float _throwDistance = 1f;
    private bool _canMove;
    private bool _isFirstThrow;
    private float _minimumAngleThreshold = 130f;
    private float _angleCoefficient = 15f;
    private float _inversion = -1f;
    private float _rotateSpeed = 0.05f;

    public UnityAction<bool> StuckInWall;

    private void Awake()
    {
        _vectorCreator = GetComponent<VectorCreator>();
    }

    private void Start()
    {
        _canMove = false;
        _isFirstThrow = true;
        _arrow.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _vectorCreator.MouseDirectionChanged += HandleMouseDirectionChange;
        _vectorCreator.DirectionSelectionCompleted += HandleDirectionSelectionCompleted;
    }

    private void OnDisable()
    {
        _vectorCreator.MouseDirectionChanged -= HandleMouseDirectionChange;
        _vectorCreator.DirectionSelectionCompleted -= HandleDirectionSelectionCompleted;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Obstacle platform))
        {
            _canMove = false;

            ÑorrectSwordWallAngle(collision);
        }
    }

    private void ÑorrectSwordWallAngle(Collision2D collision)
    {
        Vector2 wallNormal = collision.contacts[0].normal;
        float angleBetweenSwordAndWall = Vector2.SignedAngle(transform.up, wallNormal);

        if (Mathf.Abs(angleBetweenSwordAndWall) < _minimumAngleThreshold)
        {
            float rotationAngleInRadians = angleBetweenSwordAndWall * Mathf.Deg2Rad;

            StartCoroutine(RotateAroundSmoothly(_bladeEnd.position, Vector3.forward, 
                rotationAngleInRadians * _angleCoefficient * _inversion, _rotateSpeed));
        }
    }

    private IEnumerator RotateAroundSmoothly(Vector3 pivotPoint, Vector3 axis, float angle, float duration)
    {
        float elapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(angle, axis) * startRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / duration;

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            transform.RotateAround(pivotPoint, axis, angle * Time.deltaTime);

            yield return null;
        }

        transform.rotation = endRotation;
    }

    private void HandleMouseDirectionChange(Vector2 direction)
    {
        if (_isFirstThrow)
        {
            StuckInWall?.Invoke(false);
        }
        else
        {
            StuckInWall?.Invoke(true);
        }

        _direction = direction;
        CalculateAngle();
        ShowArrow();
    }

    private void HandleDirectionSelectionCompleted()
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

    public void SetDirectionAndAngle(Vector2 direction, float angle)
    {
        _direction = direction;
        _angle = angle;

        ShowArrow();
    }

    private void ShowArrow()
    {
        _arrow.gameObject.SetActive(true);
        _arrow.gameObject.transform.position = transform.position;
        _arrow.gameObject.transform.up = _direction;
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
