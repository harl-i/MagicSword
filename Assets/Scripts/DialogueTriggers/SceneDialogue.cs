using System;
using System.Collections;
using Localization;
using Sword;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace DialogueTriggers
{
    public enum Level
    {
        FirstLevel = 2,
        ThirdLevel = 8,
        FifthLevel = 14,
        SeventhLevel = 20,
    }

    public class SceneDialogue : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private DialogueWindow _dialogueWindow;
        [SerializeField] private TextMeshProUGUI _dialogueTextField;
        [SerializeField] private string[] _dialogueKeys;
        [SerializeField] private float _typingSpeed = 0.05f;
        [SerializeField] private LocalizationManager _localizationManager;

        [SerializeField] private bool _needPermanentDisableTrigger;
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private float _timeForTemporaryDisable;
        [SerializeField] private GameObject[] _auxiliaryObjects;

        [SerializeField] private GameObject _mobileUI;
        [SerializeField] private GameObject _desktopUI;
        [SerializeField] private GameObject _tapToScreenTip;

        private int _currentIndex = 0;
        private bool _isTyping = true;
        private Animator _tapTipAnimator;

        public static Action<bool> OnDialogShow;

        private void OnEnable()
        {
            _tapTipAnimator = _tapToScreenTip.GetComponent<Animator>();
            _isTyping = true;

            _dialogueWindow.WindowShown += OnDialogueWindowShown;
        }

        private void OnDisable() => _dialogueWindow.WindowShown -= OnDialogueWindowShown;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _dialogueWindow.gameObject.activeSelf)
            {
                if (!_isTyping)
                {
                    AdvanceDialogue();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (SkipIfSeenBefore())
                return;

            if (collision.TryGetComponent(out Player player))
            {
                PauseGame();
            }
        }

        public void StartDialogue()
        {
            _isTyping = true;

            if (_dialogueKeys.Length > 0)
            {
                string text = _localizationManager.GetText(_dialogueKeys[_currentIndex]);
                StartCoroutine(TypeText(text));
            }

            EnableAuxularyObjects();
        }

        private void OnDialogueWindowShown()
        {
            StartDialogue();
            OnDialogShow?.Invoke(true);
        }

        private void AdvanceDialogue()
        {
            _currentIndex++;

            if (_currentIndex < _dialogueKeys.Length)
            {
                UpdateDialogue();
            }
            else
            {
                EndDialogue();
            }
        }

        private void UpdateDialogue()
        {
            _isTyping = true;

            if (_currentIndex < _dialogueKeys.Length)
            {
                string text = _localizationManager.GetText(_dialogueKeys[_currentIndex]);
                StartCoroutine(TypeText(text));
            }
        }

        private void EndDialogue()
        {
            DisableAuxularyObjects();
            OnDialogShow?.Invoke(false);
            ResumeGame();
        }

        private void PauseGame()
        {
            _mobileUI.SetActive(false);
            _desktopUI.SetActive(false);

            Time.timeScale = 0f;
            _dialogueWindow.gameObject.SetActive(true);

            _tapTipAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _animator.SetTrigger("DialogueTrigger");
        }

        private IEnumerator TypeText(string dialogue)
        {
            ClearDialogueField();
            HideTapTip();

            foreach (char letter in dialogue.ToCharArray())
            {
                _dialogueTextField.text += letter;

                yield return new WaitForSecondsRealtime(_typingSpeed);
            }

            _isTyping = false;
            ShowTapTip();
        }

        private void ClearDialogueField() => _dialogueTextField.text = string.Empty;

        private void ResumeGame()
        {
            _mobileUI.SetActive(true);
            _desktopUI.SetActive(true);

            _animator.updateMode = AnimatorUpdateMode.Normal;
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

            HideTapTip();
            Time.timeScale = 1f;
        }

        private IEnumerator TemporaryDisableTrigger()
        {
            _trigger.enabled = false;

            yield return new WaitForSeconds(_timeForTemporaryDisable);

            _trigger.enabled = true;
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

        private bool SkipIfSeenBefore()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            return currentSceneIndex switch
            {
                (int)Level.FirstLevel => YG2.saves.FirstLevelDialogueWatch == 1,
                (int)Level.ThirdLevel => YG2.saves.ThirdLevelDialogueWatch == 1,
                (int)Level.FifthLevel => YG2.saves.FifthLevelDialogueWatch == 1,
                (int)Level.SeventhLevel => YG2.saves.SeventhLevelDialogueWatch == 1,
                _ => false,
            };
        }

        private void ShowTapTip() => _tapToScreenTip.gameObject.SetActive(true);

        private void HideTapTip() => _tapToScreenTip.gameObject.SetActive(false);
    }
}
