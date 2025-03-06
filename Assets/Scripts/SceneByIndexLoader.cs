using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByIndexLoader : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;
    [SerializeField] private float _delay = 0f;

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(_sceneIndex);
    }

    public void LoadSceneByIndex() 
    {
        StartCoroutine(LoadScene());
    }
}
