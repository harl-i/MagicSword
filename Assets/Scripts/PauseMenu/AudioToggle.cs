using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] private Image _buttonIcon;
    [SerializeField] private Sprite _volumeOn;
    [SerializeField] private Sprite _volumeOff;

    private bool _isMuted = false;

    private void Start()
    {
        _isMuted = YG2.saves.Volume == 0;
        AudioListener.volume = YG2.saves.Volume;
        UpdateIcon();
    }

    public void ToggleAudio()
    {
        _isMuted = !_isMuted;
        YG2.saves.Volume = _isMuted ? 0 : 1;
        AudioListener.volume = YG2.saves.Volume;

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (YG2.saves.Volume == 1)
        {
            _buttonIcon.sprite = _volumeOn;
        }
        else
        {
            _buttonIcon.sprite = _volumeOff;
        }
    }
}
