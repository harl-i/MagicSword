using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _dampTime = 0.4f;
    private float _minY = 0f;
    private Vector3 _cameraPos;
    private Vector3 _velocity = Vector3.zero;

    void Update()
    {
        float currentX = transform.position.x;

        _cameraPos = new Vector3(currentX, _player.position.y, _player.position.z);
        _cameraPos.y = Mathf.Clamp(_cameraPos.y, _minY, float.MaxValue);

        //transform.position = Vector3.SmoothDamp(transform.position, _cameraPos, ref _velocity, _dampTime);

        if (_player.position.x > 2.4f)
        {
            // Смещение камеры вправо, если игрок перешел через проход
            _cameraPos = new Vector2(5.6f, Mathf.Clamp(_cameraPos.y, _minY, float.MaxValue));
        }
        else if (_player.position.x < 2.4f)
        {
            _cameraPos = new Vector2(0f, Mathf.Clamp(_cameraPos.y, _minY, float.MaxValue));
        }
        //else
        //{
        //    // Обычное следование за игроком
        //    //_cameraPos = new Vector3(currentX, _player.position.y, _player.position.z);
        //    //_cameraPos.y = Mathf.Clamp(_cameraPos.y, _minY, float.MaxValue);

        //}

        transform.position = Vector3.SmoothDamp(transform.position, _cameraPos, ref _velocity, _dampTime);
        // Применяем плавное смещение к новой позиции камеры
        //transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, dampTime);
    }
}

//вариант для комнаты
//    if (Player.position.x > RoomTransitionPoint.position.x) {
//            // Смещение камеры вправо, если игрок перешел через проход
//            cameraPos = new Vector3(RoomTransitionPoint.position.x, Player.position.y, Player.position.z);
//} else
//{
//    // Обычное следование за игроком
//    cameraPos = new Vector3(transform.position.x, Player.position.y, Player.position.z);
//}

//// Применяем плавное смещение к новой позиции камеры
//transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, dampTime);
//}
