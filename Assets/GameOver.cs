using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMessage;

    private float _delay = 5f;

    private void OnEnable()
    {
        Player.HealthHasChanged += CheckPlayerHealth;
    }

    private void OnDisable()
    {
        Player.HealthHasChanged -= CheckPlayerHealth;
    }

    private void CheckPlayerHealth(int count)
    {
        if (count == 0)
        {
            StartCoroutine(ShowGameOverScreenAndExit(_delay));
        }
    }

    private IEnumerator ShowGameOverScreenAndExit(float delay)
    {
        Time.timeScale = 0;
        _gameOverMessage.SetActive(true);

        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
