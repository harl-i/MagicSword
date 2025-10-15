using UnityEngine;

namespace Sword
{
    public class CameraFollow : MonoBehaviour
    {
        private const float LevelRightEdge = 2.4f;

        [SerializeField] private Transform _player;
        [SerializeField] private float _dampTime = 0.4f;
        [SerializeField] private float _maxYMobile;
        [SerializeField] private float _maxYDesktop;

        private float MaxY = 100f;
        private float _minY;
        private Vector3 _cameraPos;
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            float currentX = transform.position.x;

            _cameraPos = new Vector3(currentX, _player.position.y, _player.position.z);
            _cameraPos.y = Mathf.Clamp(_cameraPos.y, _minY, MaxY);

            if (_player.position.x > LevelRightEdge)
            {
                _cameraPos = new Vector2(5.6f, Mathf.Clamp(_cameraPos.y, _minY, MaxY));
            }
            else if (_player.position.x < LevelRightEdge)
            {
                _cameraPos = new Vector2(0f, Mathf.Clamp(_cameraPos.y, _minY, MaxY));
            }

            transform.position = Vector3.SmoothDamp(transform.position, _cameraPos, ref _velocity, _dampTime);
        }

        public void SwitchToDesktop()
        {
            MaxY = _maxYDesktop;
            _minY = -1.24f;
        }

        public void SwitchToMobile()
        {
            MaxY = _maxYMobile;
            _minY = -1f;
        }
    }
}
