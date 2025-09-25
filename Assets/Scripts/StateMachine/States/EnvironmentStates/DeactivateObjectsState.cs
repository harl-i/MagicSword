using UnityEngine;

public class DeactivateObjectsState : State
{
    [SerializeField] private GameObject[] _objects;

    private void OnEnable()
    {
        foreach (var item in _objects)
        {
            item.gameObject.SetActive(false);
        }
    }
}
