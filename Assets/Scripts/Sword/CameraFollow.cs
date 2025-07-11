using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _dampTime = 0.4f;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;

    private Vector3 _cameraPos;
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        float currentX = transform.position.x;

        _cameraPos = new Vector3(currentX, _player.position.y, _player.position.z);
        _cameraPos.y = Mathf.Clamp(_cameraPos.y, _minY, _maxY);

        if (_player.position.x > 2.4f)
        {
            _cameraPos = new Vector2(5.6f, Mathf.Clamp(_cameraPos.y, _minY, _maxY));
        }
        else if (_player.position.x < 2.4f)
        {
            _cameraPos = new Vector2(0f, Mathf.Clamp(_cameraPos.y, _minY, _maxY));
        }

        transform.position = Vector3.SmoothDamp(transform.position, _cameraPos, ref _velocity, _dampTime);
    }

    public void SwitchToDesktop()
    {
        _minY = -1.24f;
    }

    public void SwitchToMobile()
    {
        _minY = -1f;
    }
}
