using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private bool _isDelayNeeded;
    [SerializeField] private float _delay;

    private int _nextSceneIndex;
    private int _mainMenuSceneIndex = 0;

    public void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        ShowAdvertisment(currentSceneIndex);

        int totalScenes = SceneManager.sceneCountInBuildSettings;
        _nextSceneIndex = currentSceneIndex + 1;

        UpdateLeaderboard();
        SavePlayerData();

        if (_nextSceneIndex < totalScenes && !_isDelayNeeded)
        {
            SceneManager.LoadScene(_nextSceneIndex);
        }
        else if (_nextSceneIndex < totalScenes && _isDelayNeeded)
        {
            StartCoroutine(LoadSceneWithDelay(_delay, _nextSceneIndex));
        }
        else
        {
            StartCoroutine(LoadSceneWithDelay(_delay, _mainMenuSceneIndex));
        }
    }

    private void ShowAdvertisment(int sceneIndex)
    {
        if (sceneIndex % 3 == 0)
        {
            YG2.InterstitialAdvShow();
        }
    }

    public IEnumerator LoadSceneWithDelay(float delay, int sceneIndex)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneIndex);
    }

    private void SavePlayerData()
    {
        if (_player)
        {
            YG2.saves.health = _player.Health;
            YG2.saves.sceneForContinue = _nextSceneIndex;
        }
        YG2.SaveProgress();
    }

    private void UpdateLeaderboard()
    {
        YG2.SetLeaderboard("soulsCountLeaderboard", YG2.saves.soulsCount);
    }
}
