using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private float _loadDelay = 2f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadWithDelay());
    }

    private IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(_loadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadNextLevel();
    }
}
