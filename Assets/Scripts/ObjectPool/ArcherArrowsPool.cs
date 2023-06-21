using UnityEngine;

public class ArcherArrowsPool : ObjectPool
{
    [SerializeField] private ArcherArrow _archerArrow;

    private void Awake()
    {
        Initialize(_archerArrow);
    }
}
