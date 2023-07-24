using System.Collections;
using System.Collections.Generic;
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
        Player.CoinsChanged += OnChangeCoinAmount;
    }

    private void OnDisable()
    {
        Player.CoinsChanged -= OnChangeCoinAmount;
    }

    private void OnChangeCoinAmount(int count)
    {
        _coinsCount.text = count.ToString();
    }
}
