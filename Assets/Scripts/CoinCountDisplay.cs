using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI _coinsCount;

    private void Awake()
    {
        _coinsCount = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        LevelManager.CoinsChanged += OnChangeCoinAmount;
    }

    private void OnDisable()
    {
        LevelManager.CoinsChanged -= OnChangeCoinAmount;
    }

    private void OnChangeCoinAmount(int count)
    {
        _coinsCount.text = count.ToString();
    }
}
