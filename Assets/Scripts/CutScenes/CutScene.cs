using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

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
    [SerializeField] private GameObject _skipCutscene;

    private int _currentIndex = 0;
    private int _firstCutscene = 2;
    private bool _isTyping = false;
    private bool _isResume = false;

    private Dictionary<string, System.Action> _cutsceneFlags;

    private void Start()
    {
        _cutsceneFlags = new Dictionary<string, System.Action>
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
            { "CutScene 14", () => YG2.saves.CutScene14Watched = 1 }
        };

        string sceneName = SceneManager.GetActiveScene().name;
        if (_cutsceneFlags.ContainsKey(sceneName) && IsCutsceneWatched(sceneName))
        {
            _skipCutscene.gameObject.SetActive(true);
        }
        else
        {
            YG2.saves.skipFirstCutscene = 0;
            UpdateCutscene();
            _isResume = true;
        }
    }

    private void Update()
    {

        if (_isResume)
        {
            if (!_isTyping)
            {
                AdvanceCutscene();
            }
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
        string sceneName = SceneManager.GetActiveScene().name;
        if (_cutsceneFlags.ContainsKey(sceneName))
        {
            _cutsceneFlags[sceneName].Invoke();
        }


        _nextSceneLoader.LoadScene();
    }

    private bool IsCutsceneWatched(string sceneName)
    {
        return sceneName switch
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
            _ => false
        };
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

    public void ResumeCutscene()
    {
        _skipCutscene.gameObject.SetActive(false);
        _isResume = true;
    }

    public void SkipCutscene()
    {
        EndCutscene();

        if (SceneManager.sceneCount == _firstCutscene)
        {
            YG2.saves.skipFirstCutscene = 1;
        }
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
