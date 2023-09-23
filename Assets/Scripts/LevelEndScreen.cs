using System.Collections;
using UnityEngine;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] private CoinMovementAnimationController _coinMovement;
    [SerializeField] private float _coinMovementActivationDelay;

    private int _coinsCount;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateCoinMovement(_coinMovementActivationDelay));
    }

    public void SetCoinsCount(int coinsCount)
    {
        _coinsCount = coinsCount;
    }

    private IEnumerator ActivateCoinMovement(float coinMovementActivationDelay)
    {
        yield return new WaitForSeconds(coinMovementActivationDelay);

        _coinMovement.SetRepeatCount(_coinsCount);
        _coinMovement.gameObject.SetActive(true);
    }
}
