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

    public void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        _nextSceneIndex = currentSceneIndex + 1;

        SavePlayerData();

        if (_nextSceneIndex < totalScenes && !_isDelayNeeded)
        {
            SceneManager.LoadScene(_nextSceneIndex);
        }
        else if (_nextSceneIndex < totalScenes && _isDelayNeeded)
        {
            StartCoroutine(LoadSceneWithDelay(_delay));
        }
        else
        {
            Debug.Log("Следующей сцены не существует!");
        }
    }

    public IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(_nextSceneIndex);
    }

    private void SavePlayerData()
    {
        if (_player)
        {
            YG2.saves.health = _player.Health;
            YG2.saves.sceneForContinue = _nextSceneIndex;
        }
    }
}
