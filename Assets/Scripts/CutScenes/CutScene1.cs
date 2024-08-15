using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene1 : MonoBehaviour
{
    [SerializeField] private FlashBangEffect _flashBangEffect;
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private GameObject[] _images;
    [SerializeField] private string[] _texts;
    [SerializeField] private float _typingSpeed = 0.05f;

    private int _currentIndex = 0;
    private bool _isTyping = false;

    private void Start()
    {
        UpdateCutscene();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isTyping)
        {
            AdvanceCutscene();
        }
    }

    private void UpdateCutscene()
    {
        if (_currentIndex < _images.Length && _currentIndex < _texts.Length)
        {
            if (_currentIndex != 0)
            {
                _images[_currentIndex - 1].SetActive(false);
            }

            if (_currentIndex == 4)
            {
                _flashBangEffect.FlashBanged();
            }

            _images[_currentIndex].SetActive(true);

            StartCoroutine(TypeText(_texts[_currentIndex]));
        }
    }

    private void AdvanceCutscene()
    {
        _currentIndex++;

        if (_currentIndex < _images.Length && _currentIndex < _texts.Length)
        {
            UpdateCutscene();
        }
        else
        {
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        Debug.Log("Cutscene ended");
        SceneManager.LoadScene(1);
    }

    private IEnumerator TypeText(string text)
    {
        _isTyping = true;

        _textDisplay.text = "";
        foreach (char letter in text.ToCharArray())
        {
            _textDisplay.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }

        _isTyping = false; 
    }
}
