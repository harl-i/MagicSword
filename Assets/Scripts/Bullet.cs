using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isFlip;

    public void SetFlip(bool isFlip)
    {
        _isFlip = isFlip;
    }

    private void Update()
    {
        if (_isFlip)
        {
            transform.Translate(new Vector2(0, _speed * Time.deltaTime), Space.Self);
        }
        else
        {
            transform.Translate(new Vector2(0, _speed * Time.deltaTime * -1), Space.Self);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }

        gameObject.SetActive(false);
    }
}
