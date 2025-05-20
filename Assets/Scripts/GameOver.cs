using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Image))]
public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMessage;
    [SerializeField] private GameObject _blackBackground;

    private Image _gameoverImage;

    private float _delay = 5f;

    private void Awake()
    {
        _gameoverImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Player.HealthHasChanged += CheckPlayerHealth;
    }

    private void OnDisable()
    {
        Player.HealthHasChanged -= CheckPlayerHealth;
        //_blackBackground.SetActive(false);
        //_gameoverImage.enabled = false;
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
        _blackBackground.SetActive(true);
        _gameoverImage.enabled = true;

        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
