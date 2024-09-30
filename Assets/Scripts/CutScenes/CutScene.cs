using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [SerializeField] private bool _needFlashEffect;
    [SerializeField] private int _sceneForFlashEffect;
    [SerializeField] private FlashBangEffect _flashBangEffect;

    [SerializeField] private GameObject[] _images;
    
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private string[] _texts;
    [SerializeField] private float _typingSpeed = 0.05f;

    [SerializeField] private NextSceneLoader _nextSceneLoader;

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

            if (_currentIndex == _sceneForFlashEffect && _needFlashEffect)
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

        _nextSceneLoader.LoadScene();
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
