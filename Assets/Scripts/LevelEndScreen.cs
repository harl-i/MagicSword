using System.Collections;
using UnityEngine;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] private CoinMovementObject _coinMovementObject;
    [SerializeField] private float _coinMovementActivationDelay;

    private const string RepeatCount = "repeatCount";

    private float _coinCount = 3;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateCoinMovement(_coinMovementActivationDelay));
    }

    private IEnumerator ActivateCoinMovement(float coinMovementActivationDelay)
    {
        yield return new WaitForSeconds(coinMovementActivationDelay);

        _coinMovementObject.gameObject.SetActive(true);
    }
}
