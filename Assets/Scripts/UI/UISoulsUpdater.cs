using TMPro;
using UnityEngine;
using YG;

namespace UI
{
    public class UISoulsUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _soulsCountUI;

        private void OnEnable()
        {
            _soulsCountUI.text = YG2.saves.SoulsCount.ToString();
        }
    }
}