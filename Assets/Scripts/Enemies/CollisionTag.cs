using UnityEngine;

namespace Enemies
{
    public class CollisionTag : MonoBehaviour
    {
        public enum Values
        {
            ArcherArrow,
            Bird,
            Bull,
        }

        public Values Value { get; private set; }
    }
}