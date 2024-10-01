using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private bool _isDelayNeeded;
    [SerializeField] private float _delay;

    private int _nextSceneIndex;

    public void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        _nextSceneIndex = currentSceneIndex + 1;

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
}
