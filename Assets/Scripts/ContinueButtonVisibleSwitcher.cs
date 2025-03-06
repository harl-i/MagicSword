using UnityEngine;
using YG;

public class ContinueButtonVisibleSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _continueButton;
    private int _noProgress = 0;

    private void Start()
    {
        if (YG2.saves.sceneForContinue == _noProgress)
        {
            _continueButton.SetActive(false);
        } else
        {
            _continueButton.SetActive(true);
        }
    }
}
