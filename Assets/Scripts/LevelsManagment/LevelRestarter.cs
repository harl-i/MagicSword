using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestarter : MonoBehaviour
{
    private int _currentSceneIndex;

    public void Restart()
    {
        Time.timeScale = 1;
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentSceneIndex);
    }
}
