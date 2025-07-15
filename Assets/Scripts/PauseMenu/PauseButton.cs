using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _mainGamePanel;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _closeSprite;

    private bool _isOpen = false;
    private Button _button;
    private Image _buttonImage;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
    }

    private void OnEnable()
    {
        SceneDialogue.OnDialogShow += HandleDialogueShow;
    }

    private void OnDisable()
    {
        SceneDialogue.OnDialogShow -= HandleDialogueShow;
    }

    private void HandleDialogueShow(bool isShow)
    {
        if (isShow)
        {
            _buttonImage.enabled = false;
        }
        else
        {
            _buttonImage.enabled = true;
        }
    }

    public void ToggleMenu()
    {
        if (_isOpen)
        {
            Hidemenu();
            _isOpen = false;
            _buttonImage.sprite = _pauseSprite;
            Time.timeScale = 1;
        }
        else
        {
            OpenMenu();
            _isOpen = true;
            _buttonImage.sprite = _closeSprite;
            Time.timeScale = 0;
        }
    }

    private void OpenMenu()
    {
        _pausePanel.SetActive(true);
        _mainGamePanel.SetActive(false);
    }

    private void Hidemenu()
    {
        _pausePanel.SetActive(false);
        _mainGamePanel.SetActive(true);
    }
}
