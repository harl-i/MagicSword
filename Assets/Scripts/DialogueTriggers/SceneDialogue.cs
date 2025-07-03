using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class SceneDialogue : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DialogueWindow _dialogueWindow;
    [SerializeField] private TextMeshProUGUI _dialogueTextField;
    [SerializeField] private string[] _textRu;
    [SerializeField] private string[] _textEn;
    [SerializeField] private string[] _textTr;
    [SerializeField] private float _typingSpeed = 0.05f;

    [SerializeField] private bool _needPermanentDisableTrigger;
    [SerializeField] private Collider2D _trigger;
    [SerializeField] private float _timeForTemporaryDisable;
    [SerializeField] private GameObject[] _auxiliaryObjects;

    private const string RU = "ru";
    private const string EN = "en";
    private const string TR = "tr";

    private int _currentIndex = 0;
    private bool _isTyping = false;
    private int _typedCharCount = 0;
    private int _charCountToUnlockSkip = 5;
    private string _lang;

    private void OnEnable()
    {
        _lang = YG2.lang;

        _dialogueWindow.WindowShown += OnDialogueWindowShown;
    }

    private void OnDisable()
    {
        _dialogueWindow.WindowShown -= OnDialogueWindowShown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dialogueWindow.gameObject.activeSelf)
        {
            if (_isTyping)
            {
                if (_typedCharCount > _charCountToUnlockSkip)
                {
                    ShowFullText();
                }
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PauseGame();
        }
    }

    public void StartDialogue()
    {
        if (_lang == RU)
        {
            StartCoroutine(TypeText(_textRu[_currentIndex]));
        }

        if (_lang == EN)
        {
            StartCoroutine(TypeText(_textEn[_currentIndex]));
        }

        if (_lang == TR)
        {
            StartCoroutine(TypeText(_textTr[_currentIndex]));
        }

        EnableAuxularyObjects();
    }

    private void OnDialogueWindowShown()
    {
        StartDialogue();
    }


    private void AdvanceDialogue()
    {
        _currentIndex++;

        if (_lang == RU)
        {
            if (_currentIndex < _textRu.Length)
            {
                UpdateDialogue();
            }
            else if (_currentIndex == _textRu.Length)
            {
                EndDialogue();
            }
        }

        if (_lang == EN)
        {
            if (_currentIndex < _textEn.Length)
            {
                UpdateDialogue();
            }
            else if (_currentIndex == _textEn.Length)
            {
                EndDialogue();
            }
        }

        if (_lang == TR)
        {
            if (_currentIndex < _textTr.Length)
            {
                UpdateDialogue();
            }
            else if (_currentIndex == _textTr.Length)
            {
                EndDialogue();
            }
        }
    }

    private void UpdateDialogue()
    {
        if (_lang == RU)
        {
            if (_currentIndex < _textRu.Length)
            {
                StartCoroutine(TypeText(_textRu[_currentIndex]));
            }
        }

        if (_lang == EN)
        {
            if (_currentIndex < _textEn.Length)
            {
                StartCoroutine(TypeText(_textEn[_currentIndex]));
            }
        }

        if (_lang == TR)
        {
            if (_currentIndex < _textRu.Length)
            {
                StartCoroutine(TypeText(_textTr[_currentIndex]));
            }
        }
    }

    private void EndDialogue()
    {
        DisableAuxularyObjects();
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _dialogueWindow.gameObject.SetActive(true);

        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _animator.SetTrigger("DialogueTrigger");
    }

    private IEnumerator TypeText(string dialogue)
    {
        ClearDialogueField();
        _isTyping = true;
        _typedCharCount = 0;

        foreach (char letter in dialogue.ToCharArray())
        {
            _dialogueTextField.text += letter;
            _typedCharCount++;

            yield return new WaitForSecondsRealtime(_typingSpeed);
        }

        _isTyping = false;
    }

    private void ClearDialogueField()
    {
        _dialogueTextField.text = "";
    }

    private void ResumeGame()
    {

        _animator.updateMode = AnimatorUpdateMode.Normal;
        _dialogueWindow.gameObject.SetActive(false);

        if (_needPermanentDisableTrigger)
        {
            _trigger.enabled = false;
        }
        else
        {
            StartCoroutine(TemporaryDisableTrigger());
        }

        ClearDialogueField();
        _currentIndex = 0;

        Time.timeScale = 1f;
    }

    private IEnumerator TemporaryDisableTrigger()
    {
        _trigger.enabled = false;

        yield return new WaitForSeconds(_timeForTemporaryDisable);

        _trigger.enabled = true;
    }

    private void ShowFullText()
    {
        StopAllCoroutines();

        if (_lang == RU)
        {
            _dialogueTextField.text = _textRu[_currentIndex];
        }

        if (_lang == EN)
        {
            _dialogueTextField.text = _textEn[_currentIndex];
        }

        if (_lang == TR)
        {
            _dialogueTextField.text = _textTr[_currentIndex];
        }

        _isTyping = false;
        _typedCharCount = 0;
    }

    private void EnableAuxularyObjects()
    {
        if (_auxiliaryObjects != null)
        {
            foreach (var item in _auxiliaryObjects)
            {
                item.SetActive(true);
            }
        }
    }

    private void DisableAuxularyObjects()
    {
        if (_auxiliaryObjects != null)
        {
            foreach (var item in _auxiliaryObjects)
            {
                item.SetActive(false);
            }
        }
    }
}
