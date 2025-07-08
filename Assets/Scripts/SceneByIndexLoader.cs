using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByIndexLoader : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_sceneIndex);
    }

}
