using UnityEngine;

public interface IObjectFromPool
{
    public GameObject GameObject
    {
        get { return GetGameObject(); }
    }

    public void ReturnToPool();

    public GameObject GetGameObject();
}
