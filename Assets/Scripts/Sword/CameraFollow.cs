using UnityEngine;

namespace Sword
{
    public class CameraFollow : MonoBehaviour
    {
        private const float LevelRightEdge = 2.4f;
        private const float MinYDesktop = -1.24f;
        private const float MinYMobile = -1f;
        private const float CameraShiftRightPosition = 5.6f;
        private const float CameraNormalPosition = 0f;

        [SerializeField] private Transform _player;
        [SerializeField] private float _dampTime = 0.4f;
        [SerializeField] private float _maxYMobile;
        [SerializeField] private float _maxYDesktop;

        private float _maxY = 100f;
        private float _minY;
        private Vector3 _cameraPos;
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            float currentX = transform.position.x;

            _cameraPos = new Vector3(currentX, _player.position.y, _player.position.z);
            _cameraPos.y = Mathf.Clamp(_cameraPos.y, _minY, _maxY);

            if (_player.position.x > LevelRightEdge)
            {
                _cameraPos = new Vector2(CameraShiftRightPosition, Mathf.Clamp(_cameraPos.y, _minY, _maxY));
            }
            else if (_player.position.x < LevelRightEdge)
            {
                _cameraPos = new Vector2(CameraNormalPosition, Mathf.Clamp(_cameraPos.y, _minY, _maxY));
            }

            transform.position = Vector3.SmoothDamp(transform.position, _cameraPos, ref _velocity, _dampTime);
        }

        public void SwitchToDesktop()
        {
            _maxY = _maxYDesktop;
            _minY = MinYDesktop;
        }

        public void SwitchToMobile()
        {
            _maxY = _maxYMobile;
            _minY = MinYMobile;
        }
    }
}
