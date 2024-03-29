using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        transform.position = _player.transform.position;
        transform.rotation = _player.transform.rotation;
    }
}
