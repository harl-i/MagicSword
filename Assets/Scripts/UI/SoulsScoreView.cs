using Portals;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SoulsScoreView : MonoBehaviour
    {
        [SerializeField] private Portal _portal;

        [SerializeField] private TextMeshProUGUI _currentSoulsAmount;
        [SerializeField] private TextMeshProUGUI _soulsAmountForActivation;

        private void Start()
        {
            _soulsAmountForActivation.text = _portal.SoulsAmountForActivation.ToString();
        }

        private void OnEnable()
        {
            _portal.SoulsChanged += OnSoulsAmountChanged;
        }

        private void OnDisable()
        {
            _portal.SoulsChanged -= OnSoulsAmountChanged;
        }

        private void OnSoulsAmountChanged(int amount)
        {
            _currentSoulsAmount.text = amount.ToString();
        }
    }
}