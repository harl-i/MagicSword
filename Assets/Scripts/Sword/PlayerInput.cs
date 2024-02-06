using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SwordThrow))]
[RequireComponent(typeof(LineRenderer))]
public class PlayerInput : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private SwordThrow _swordThrow;
    private LineRenderer lineRenderer;
    //private Vector2 _lastMousePosition;
    //private Vector2 _currentMousePosition;

    private Vector3 startPosition;
    private Vector3 currentPosition;

    private Vector3 _direction;
    private float _angle;

    private void Awake()
    {
        _swordThrow = GetComponent<SwordThrow>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        startPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        currentPosition = eventData.position;
        lineRenderer.SetPosition(1, currentPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        lineRenderer.SetPosition(1, currentPosition);
        lineRenderer.SetPosition(2, eventData.position);
    }

    //private void Update()
    //{

    //    if (Input.GetMouseButton(0))
    //    {
    //        _currentMousePosition = Input.mousePosition;
    //        Vector2 vector = _currentMousePosition - _lastMousePosition;
    //        DrawVector(vector);

    //        Vector3 worldVector = Camera.main.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 0f)) - transform.position;
    //        float angle = Vector3.SignedAngle(worldVector, transform.right, Vector3.forward);

    //        //CalculateDirection();
    //        //CalculateAngle();

    //        SetDirectionAndAngle(_direction, _angle);

    //        _lastMousePosition = _currentMousePosition;
    //    }
    //}

    //private void DrawVector(Vector2 vector)
    //{
    //    // Установка количества точек в LineRenderer
    //    _lineRenderer.positionCount = 2;

    //    // Установка начальной точки в текущую позицию объекта
    //    _lineRenderer.SetPosition(0, _lastMousePosition);

    //    // Установка конечной точки, сдвинутой на вектор
    //    _lineRenderer.SetPosition(1, _currentMousePosition);

    //    // Выбор цвета линии
    //    _lineRenderer.startColor = Color.red;
    //    _lineRenderer.endColor = Color.green;

    //    // Настройка ширины линии
    //    _lineRenderer.startWidth = 0.3f;
    //    _lineRenderer.endWidth = 0.3f;
    //}

    ////private void CalculateDirection()
    ////{
    ////    _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    ////    _direction.z = 0f;
    ////    _direction.Normalize();
    ////}

    ////private void CalculateAngle()
    ////{
    ////    _angle = Vector2.SignedAngle(transform.InverseTransformDirection(transform.up), _direction);
    ////}

    public void SetDirectionAndAngle(Vector3 direction, float angle)
    {
        if (_swordThrow != null)
        {
            _swordThrow.SetDirectionAndAngle(direction, angle);
        }
    }

}
