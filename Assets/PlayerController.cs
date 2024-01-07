using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 5f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 nextPosition = transform.position;
            nextPosition.y = transform.position.y + _speed * Time.deltaTime;
            transform.position = nextPosition;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 nextPosition = transform.position;
            nextPosition.x = transform.position.x - _speed * Time.deltaTime;
            transform.position = nextPosition;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 nextPosition = transform.position;
            nextPosition.x = transform.position.x + _speed * Time.deltaTime;
            transform.position = nextPosition;
        }
    }
}
