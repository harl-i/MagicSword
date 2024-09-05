using System.Collections;
using TMPro;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DialogueWindow _dialogueWindow;
    [SerializeField] private TextMeshProUGUI _dialogueTextField;
    [SerializeField] private string[] _text;
    [SerializeField] private float _typingSpeed = 0.05f;

    [SerializeField] private bool _needPermanentDisableTrigger;
    [SerializeField] private Collider2D _trigger;
    [SerializeField] private float _timeForTemporaryDisable;

    private int _currentIndex = 0;
    private bool _isTyping = false;

    private void OnEnable()
    {
        _dialogueWindow.WindowShown += OnDialogueWindowShown;
    }

    private void OnDisable()
    {
        _dialogueWindow.WindowShown -= OnDialogueWindowShown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isTyping && _dialogueWindow.gameObject.activeSelf)
        {
            AdvanceDialogue();
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
        StartCoroutine(TypeText(_text[_currentIndex]));
    }

    private void OnDialogueWindowShown()
    {
        StartDialogue();
    }


    private void AdvanceDialogue()
    {
        _currentIndex++;

        if (_currentIndex < _text.Length)
        {
            UpdateDialogue();
        }
        else if (_currentIndex == _text.Length)
        {
            EndDialogue();
        }
    }

    private void UpdateDialogue()
    {
        if (_currentIndex < _text.Length)
        {
            StartCoroutine(TypeText(_text[_currentIndex]));
        }
    }

    private void EndDialogue()
    {
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

        foreach (char letter in dialogue.ToCharArray())
        {
            _dialogueTextField.text += letter;
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
}
