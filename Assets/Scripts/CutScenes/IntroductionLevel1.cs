using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroductionLevel1 : MonoBehaviour
{
    [SerializeField] private GameObject _startDialogueCanvas;
    [SerializeField] private GameObject _ui;
    [SerializeField] private FlashBangEffect _flashBangEffect;
    
    [Space]
    [Header("Curtain")]
    [SerializeField] private SpriteRenderer _curtain;
    [SerializeField] private float _fadeSpeed;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private string[] _texts;

    private int _currentIndex = 0;
    private bool _isTyping = false;

    private void Start()
    {
        UpdateCutscene();
    }

    

    private void Update()
    {
        if (!_isTyping)
        //if (Input.GetMouseButtonDown(0) && !_isTyping)
        {
            AdvanceCutscene();
        }
    }

    private void UpdateCutscene()
    {
        if (_currentIndex < _texts.Length)
        {
            if (_currentIndex == 0)
            {
                _flashBangEffect.FlashBanged();
            }

            StartCoroutine(TypeText(_texts[_currentIndex]));
        }
    }

    private void AdvanceCutscene()
    {
        _currentIndex++;

        if (_currentIndex < _texts.Length)
        {
            UpdateCutscene();
        }
        else if(_currentIndex == _texts.Length)
        {
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        Debug.Log("Cutscene ended");
        _startDialogueCanvas.gameObject.SetActive(false);
        StartCoroutine(FadeOut());
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

        yield return new WaitForSeconds(1);
        _isTyping = false;
    }

    private IEnumerator FadeOut()
    {
        Color color = _curtain.color;
        float startAlpha = color.a;
        float timeElapsed = 0f;
        float duration = 1f / _fadeSpeed;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / duration;

            color.a = startAlpha * (1 - Mathf.Pow(normalizedTime, 2));

            _curtain.color = color;

            if (color.a < 0.8)
            {
                _ui.SetActive(true);
            }

            yield return null;
        }

        color.a = 0;
        _curtain.color = color;
    }
}
