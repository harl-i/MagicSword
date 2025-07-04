using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class ContinuesShew : MonoBehaviour
{
    [SerializeField] private GameObject _continuesPanel;
    [SerializeField] private TMP_Text _continueTMP;
    [SerializeField] private float _delay;

    private int _continuesCount;

    private void OnEnable()
    {
        StartCoroutine(DelayBeforeDisable());
        _continuesCount = YG2.saves.continues + 1;
        _continueTMP.text = _continuesCount.ToString();
    }


    public void Subtract()
    {
       _continueTMP.text = YG2.saves.continues.ToString();
    }

    private IEnumerator DelayBeforeDisable()
    {
        yield return new WaitForSeconds(_delay);
        _continuesPanel.SetActive(false);
    }
}
