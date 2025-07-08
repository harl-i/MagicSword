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
    [SerializeField] private GameObject _continueScreen;

    private Image _gameoverImage;

    private float _delay = 3.4f;

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
    }

    private void CheckPlayerHealth(int count)
    {
        if (count == 0)
        {
            YG2.saves.continues--;
            if (YG2.saves.continues == 0)
            {
                StartCoroutine(ShowGameOverScreenAndExit(_delay));
            }
        }
    }

    private IEnumerator ShowGameOverScreenAndExit(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 0;
        _continueScreen.SetActive(false);
        _gameOverMessage.SetActive(true);
        _blackBackground.SetActive(true);
        _gameoverImage.enabled = true;

        yield return new WaitForSecondsRealtime(delay - 2);

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
