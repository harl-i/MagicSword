using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class IntroductionLevel1 : MonoBehaviour
{
    [SerializeField] private GameObject _startDialogueCanvas;
    [SerializeField] private GameObject _uiMobile;
    [SerializeField] private GameObject _uiDesktop;
    [SerializeField] private FlashBangEffect _flashBangEffect;
    [SerializeField] private GameObject _tapToScreenTip;

    [Space]
    [Header("Curtain")]
    [SerializeField] private SpriteRenderer _curtain;
    [SerializeField] private float _fadeSpeed;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private string[] _textsRu;
    [SerializeField] private string[] _textsEn;
    [SerializeField] private string[] _textsTr;

    [Header("UI Switch")]
    [SerializeField] private EnviromentSwitcher _enviromentSwitcher;

    [Header("UI Move Hint")]
    [SerializeField] private GameObject _moveHint;

    private int _currentIndex = 0;
    private bool _isTyping = false;
    private string _lang;
    private bool _hasAlreadyActivated = false;

    private const string RU = "ru";
    private const string EN = "en";
    private const string TR = "tr";

    private void Start()
    {
        _lang = YG2.lang;

        UpdateCutscene();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isTyping)
            {
                AdvanceCutscene();
            }
        }
    }

    private void UpdateCutscene()
    {
        if (YG2.saves.SkipFirstCutscene == 1)
        {
            Time.timeScale = 1;
            StartCoroutine(FadeOut());
            return;
        }

        if (_lang == RU)
        {
            if (_currentIndex < _textsRu.Length)
            {
                if (_currentIndex == 0)
                {
                    _flashBangEffect.FlashBanged();
                }

                StartCoroutine(TypeText(_textsRu[_currentIndex]));
            }
        }

        if (_lang == EN)
        {
            if (_currentIndex < _textsEn.Length)
            {
                if (_currentIndex == 0)
                {
                    _flashBangEffect.FlashBanged();
                }

                StartCoroutine(TypeText(_textsEn[_currentIndex]));
            }
        }

        if (_lang == TR)
        {
            if (_currentIndex < _textsTr.Length)
            {
                if (_currentIndex == 0)
                {
                    _flashBangEffect.FlashBanged();
                }

                StartCoroutine(TypeText(_textsTr[_currentIndex]));
            }
        }
    }

    private void AdvanceCutscene()
    {
        _currentIndex++;

        if (_currentIndex < _textsRu.Length)
        {
            UpdateCutscene();
        }
        else if (_currentIndex == _textsRu.Length)
        {
            HideTapTip();
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        _startDialogueCanvas.gameObject.SetActive(false);
        StartCoroutine(FadeOut());
    }

    private IEnumerator TypeText(string text)
    {
        _isTyping = true;
        HideTapTip();

        _textDisplay.text = string.Empty;
        foreach (char letter in text.ToCharArray())
        {
            _textDisplay.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }

        yield return new WaitForSeconds(1);
        _isTyping = false;
        ShowTapTip();
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
                _enviromentSwitcher.gameObject.SetActive(true);

                Debug.Log(_moveHint.activeSelf);
                if (!_hasAlreadyActivated)
                {
                    _moveHint.SetActive(true);
                    _hasAlreadyActivated = true;
                }
            }

            yield return null;
        }

        color.a = 0;
        _curtain.color = color;
        gameObject.SetActive(false);
    }

    private void ShowTapTip()
    {
        _tapToScreenTip.gameObject.SetActive(true);
    }

    private void HideTapTip()
    {
        _tapToScreenTip.gameObject.SetActive(false);
    }
}
