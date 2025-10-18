using System.Collections;
using System.Collections.Generic;
using LevelsManagment;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visuals;
using YG;

namespace CutScenes
{
    public class CutScene : MonoBehaviour
    {
        [SerializeField] private bool _needFlashEffect;
        [SerializeField] private int _sceneForFlashEffect;
        [SerializeField] private FlashBangEffect _flashBangEffect;

        [SerializeField] private CutsceneFrame[] _frames;

        [SerializeField] private TextMeshProUGUI _textDisplay;
        [SerializeField] private string[] _dialogueKeys;
        [SerializeField] private float _typingSpeed = 0.05f;
        [SerializeField] private LocalizationManager _localizationManager;

        [SerializeField] private NextSceneLoader _nextSceneLoader;
        [SerializeField] private GameObject _skipCutscene;

        [SerializeField] private GameObject _tapToScreenTip;

        private int _currentIndex = 0;
        private int _firstCutscene = 2;
        private bool _isTyping = false;

        private Dictionary<string, System.Action> _cutsceneFlags;

        private void Start()
        {
            InitializeCutsceneFlags();

            string sceneName = SceneManager.GetActiveScene().name;
            ShowFrame(_currentIndex);
            if (_cutsceneFlags.ContainsKey(sceneName) && IsCutsceneWatched(sceneName))
            {
                _skipCutscene.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                YG2.saves.SkipFirstCutscene = 0;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isTyping)
                {
                    AdvanceFrame();
                }
            }
        }

        private void AdvanceFrame()
        {
            _currentIndex++;
            if (_currentIndex < _frames.Length)
            {
                ShowFrame(_currentIndex);
            }
            else
            {
                EndCutscene();
            }
        }

        private void ShowFrame(int index)
        {
            if (index < 0 || index >= _frames.Length)
            {
                EndCutscene();
                return;
            }

            HideTapTip();
            _textDisplay.text = string.Empty;

            foreach (var frame in _frames)
            {
                if (frame.ImageObject != null)
                    frame.ImageObject.SetActive(false);
            }

            var currentFrame = _frames[index];

            if (currentFrame.ImageObject != null)
            {
                currentFrame.ImageObject.SetActive(true);
            }

            if (_needFlashEffect && index == _sceneForFlashEffect)
            {
                _flashBangEffect.FlashBanged();
            }

            if (!string.IsNullOrEmpty(currentFrame.TextKey))
            {
                string localizedText = _localizationManager.GetText(currentFrame.TextKey);
                StartCoroutine(TypeText(localizedText));
            }
            else
            {
                _isTyping = false;
                ShowTapTip();
            }
        }

        private void EndCutscene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (_cutsceneFlags.ContainsKey(sceneName))
            {
                _cutsceneFlags[sceneName].Invoke();
            }

            Time.timeScale = 1;
            _nextSceneLoader.LoadScene();
        }

        private IEnumerator TypeText(string text)
        {
            _isTyping = true;

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

        public void ResumeCutscene()
        {
            _skipCutscene.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void SkipCutscene()
        {
            EndCutscene();

            if (SceneManager.sceneCount == _firstCutscene)
            {
                YG2.saves.SkipFirstCutscene = 1;
            }
        }

        private void ShowTapTip() => _tapToScreenTip.gameObject.SetActive(true);

        private void HideTapTip() => _tapToScreenTip.gameObject.SetActive(false);

        private void InitializeCutsceneFlags() => _cutsceneFlags = new Dictionary<string, System.Action>
            {
                { "CutScene 1", () => YG2.saves.CutScene1Watched = 1 },
                { "CutScene 2", () => YG2.saves.CutScene2Watched = 1 },
                { "CutScene 3", () => YG2.saves.CutScene3Watched = 1 },
                { "CutScene 4", () => YG2.saves.CutScene4Watched = 1 },
                { "CutScene 5", () => YG2.saves.CutScene5Watched = 1 },
                { "CutScene 6", () => YG2.saves.CutScene6Watched = 1 },
                { "CutScene 7", () => YG2.saves.CutScene7Watched = 1 },
                { "CutScene 8", () => YG2.saves.CutScene8Watched = 1 },
                { "CutScene 9", () => YG2.saves.CutScene9Watched = 1 },
                { "CutScene 10", () => YG2.saves.CutScene10Watched = 1 },
                { "CutScene 11", () => YG2.saves.CutScene11Watched = 1 },
                { "CutScene 12", () => YG2.saves.CutScene12Watched = 1 },
                { "CutScene 13", () => YG2.saves.CutScene13Watched = 1 },
                { "CutScene 14", () => YG2.saves.CutScene14Watched = 1 },
            };

        private bool IsCutsceneWatched(string sceneName) => sceneName switch
        {
            "CutScene 1" => YG2.saves.CutScene1Watched == 1,
            "CutScene 2" => YG2.saves.CutScene2Watched == 1,
            "CutScene 3" => YG2.saves.CutScene3Watched == 1,
            "CutScene 4" => YG2.saves.CutScene4Watched == 1,
            "CutScene 5" => YG2.saves.CutScene5Watched == 1,
            "CutScene 6" => YG2.saves.CutScene6Watched == 1,
            "CutScene 7" => YG2.saves.CutScene7Watched == 1,
            "CutScene 8" => YG2.saves.CutScene8Watched == 1,
            "CutScene 9" => YG2.saves.CutScene9Watched == 1,
            "CutScene 10" => YG2.saves.CutScene10Watched == 1,
            "CutScene 11" => YG2.saves.CutScene11Watched == 1,
            "CutScene 12" => YG2.saves.CutScene12Watched == 1,
            "CutScene 13" => YG2.saves.CutScene13Watched == 1,
            "CutScene 14" => YG2.saves.CutScene14Watched == 1,
            _ => false,
        };
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public int CutScene1Watched = 0;
        public int CutScene2Watched = 0;
        public int CutScene3Watched = 0;
        public int CutScene4Watched = 0;
        public int CutScene5Watched = 0;
        public int CutScene6Watched = 0;
        public int CutScene7Watched = 0;
        public int CutScene8Watched = 0;
        public int CutScene9Watched = 0;
        public int CutScene10Watched = 0;
        public int CutScene11Watched = 0;
        public int CutScene12Watched = 0;
        public int CutScene13Watched = 0;
        public int CutScene14Watched = 0;
    }
}
