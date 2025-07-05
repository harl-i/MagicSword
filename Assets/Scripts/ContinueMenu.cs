using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ContinueMenu : MonoBehaviour
{
    [SerializeField] private int _soulsForContinue;
    [SerializeField] private GameObject _error;
    [SerializeField] private int _delay;
    [SerializeField] private GameObject _mainMenu;

    private void OnEnable()
    {
        _mainMenu.SetActive(false);
    }

    public void Close()
    {
        _mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ContinueForSouls()
    {
        if (YG2.saves.soulsCount >= _soulsForContinue)
        {
            YG2.saves.soulsCount -= _soulsForContinue;
            ResetContinues();
            UpdateLeaderboard();
            LoadContinueLevel();
        }
        else
        {
            StartCoroutine(ShowError(_delay));
        }
    }

    public void ContinueForAdvertisment()
    {
        ResetContinues();
        ShowAdvReward();
    }

    private void ShowAdvReward()
    {
        string id = "continue";
        YG2.RewardedAdvShow(id, LoadContinueLevel);
    }

    private void LoadContinueLevel()
    {
        SceneManager.LoadScene(YG2.saves.sceneForContinue);
    }

    private IEnumerator ShowError(int delay)
    {
        _error.SetActive(true);
        yield return new WaitForSeconds(delay);
        _error.SetActive(false);
    }

    private void UpdateLeaderboard()
    {
        YG2.SetLeaderboard("soulsCountLeaderboard", YG2.saves.soulsCount);
    }

    private void ResetContinues()
    {
        YG2.saves.continues = 3;
    }
}
