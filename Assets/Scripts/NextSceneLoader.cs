using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using YG.Utils.LB;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private bool _isDelayNeeded;
    [SerializeField] private float _delay;

    private const string SOULS_LEADERBOARD = "soulsCountLeaderboard";

    private int _nextSceneIndex;
    private int _mainMenuSceneIndex = 0;
    private int _playerScoreInLeaderboard;
    private bool _isLeaderboardUpdating;

    private void OnEnable()
    {
        YG2.onGetLeaderboard += OnLeaderboardReceived;
    }

    private void OnDisable()
    {
        YG2.onGetLeaderboard -= OnLeaderboardReceived;
    }

    public void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SetSeenDialogueTrigger(currentSceneIndex);
        
        ShowAdvertisment(currentSceneIndex);

        int totalScenes = SceneManager.sceneCountInBuildSettings;
        _nextSceneIndex = currentSceneIndex + 1;

        SavePlayerData();
        UpdateLeaderboard();

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
        if (sceneIndex % 4 == 0)
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
        if (_isLeaderboardUpdating) return;

        _isLeaderboardUpdating = true;
        YG2.GetLeaderboard(SOULS_LEADERBOARD);
    }

    private void OnLeaderboardReceived(LBData data)
    {
        _isLeaderboardUpdating = false;
        if (data.technoName == SOULS_LEADERBOARD)
        {
            if (data.currentPlayer != null)
            {
                _playerScoreInLeaderboard = data.currentPlayer.score;

                if (_playerScoreInLeaderboard < YG2.saves.soulsCount && YG2.saves.soulsCount > 0)
                {
                    YG2.SetLeaderboard(SOULS_LEADERBOARD, YG2.saves.soulsCount);
                }
            }
        }
    }

    private void SetSeenDialogueTrigger(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case (int)level.firstLevel:
                if (YG2.saves.firstLevelDialogueWatch == 0) YG2.saves.firstLevelDialogueWatch = 1;
                break;
            case (int)level.thirdLevel:
                if (YG2.saves.thirdLevelDialogueWatch == 0) YG2.saves.thirdLevelDialogueWatch = 1;
                break;
            case (int)level.fifthLevel:
                if (YG2.saves.fifthLevelDialogueWatch == 0) YG2.saves.fifthLevelDialogueWatch = 1;
                break;
            case (int)level.seventhLevel:
                if (YG2.saves.seventhLevelDialogueWatch == 0) YG2.saves.seventhLevelDialogueWatch = 1;
                break;
            default:
                break;
        }
    }
}
