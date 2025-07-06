using UnityEngine;
using YG;

public class FirstPlayButtonVisibleSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private GameObject _newGamePlus;
    [SerializeField] private GameObject _newGame;
    private int _noProgress = 0;

    private void Start()
    {
        if (YG2.saves.sceneForContinue == _noProgress)
        {
            _continueButton.SetActive(false);
            _newGamePlus.SetActive(false);
            _newGame.SetActive(true);
        } else
        {
            _continueButton.SetActive(true);
            _newGamePlus.SetActive(true);
            _newGame.SetActive(false);
        }
    }
}
