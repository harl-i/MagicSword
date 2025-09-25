using UnityEngine;

namespace Sword
{
    public class PositionUpdater : MonoBehaviour
    {
        [SerializeField] private Transform _objectForCopyPosition;

        private void Update()
        {
            transform.position = _objectForCopyPosition.position;
        }
    }
}