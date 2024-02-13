using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapperActivator : MonoBehaviour
{
    public Action AllSpritesSwapped;

    [SerializeField] private float _delayBeforeDeactivateSprite;

    private List<SpriteSwapper> _swappers = new List<SpriteSwapper>();

    private void Awake()
    {
        FindAndStoreSwappers();
    }

    private void FindAndStoreSwappers()
    {
        foreach (Transform child in transform)
        {
            SpriteSwapper swapper = child.GetComponent<SpriteSwapper>();
            if (swapper != null)
            {
                _swappers.Add(swapper);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            foreach (var swapper in _swappers)
            {
                swapper.enabled = true;
            }
        }

        StartCoroutine(DelayBeforeInvoke(_delayBeforeDeactivateSprite));
    }

    private IEnumerator DelayBeforeInvoke(float delay)
    {
        yield return new WaitForSeconds(delay);

        AllSpritesSwapped?.Invoke();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out Player player))
    //    {
    //        foreach (var swapper in _swappers)
    //        {
    //            swapper.enabled = true;
    //        }
    //    }
    //}
}
