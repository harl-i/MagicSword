using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Scull : MonoBehaviour
{
    private Image _image;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void EnemyDied()
    {
        IsDead = true;
        _image.color = Color.red;
    }
}
