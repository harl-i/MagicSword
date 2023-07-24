using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private LayerMask _coinLayer;
    [SerializeField] private List<GameObject> _coins = new List<GameObject>();

    private const string WasSwordTouchChest = "wasSwordTouchChest";
    private float _coinDropDelayTime = 0.5f;
    private Animator _animator;
    private bool _isClosed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _isClosed = true;

        foreach (Transform child in transform)
        {
            _coins.Add(child.gameObject);
        }

        foreach (var coin in _coins)
        {
            if ((_coinLayer.value & (1 << coin.layer)) != 0)
            {
                foreach (var coinForIgnore in _coins)
                {
                    Physics2D.IgnoreCollision(coin.GetComponent<CircleCollider2D>(), coinForIgnore.GetComponent<CircleCollider2D>());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player magicSword) && _isClosed == true)
        {
            StartCoroutine(OpenChest(magicSword));
            magicSword.IncreaseCoinsCount(_coins.Count);
        }
    }

    private IEnumerator OpenChest(Player player)
    {
        _animator.SetBool(WasSwordTouchChest, true);
        _isClosed = false;

        yield return new WaitForSeconds(_coinDropDelayTime);

        foreach (var coin in _coins)
        {
            coin.SetActive(true);
            coin.GetComponent<Coin>().SetTarget(player.transform);
        }
    }
}
