using UnityEngine;

public class TrollleyTrap : MonoBehaviour 
{
    [SerializeField] private PolygonCollider2D _mainTrolleyCollider;

    public PolygonCollider2D GetMainTrolleyCollider()
    {
        return _mainTrolleyCollider;
    }
}
