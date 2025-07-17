using UnityEngine;
using YG;

public class RestartState : State
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private Player _player;
    [SerializeField] private SpriteRenderer _swordSprite;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Quaternion _startRotation = Quaternion.identity;
    [SerializeField] private GameObject _UIContinueScreen;

    private void OnEnable()
    {
        _UIContinueScreen.SetActive(true);

        if (YG2.saves.Continues > 0)
        {
            _playerAnimator.Rebind();
            _playerAnimator.Update(0f);

            _player.FullHealing();
            _playerObject.transform.localPosition = Vector3.zero;
            _playerObject.transform.rotation = _startRotation;
            _playerObject.transform.localScale = Vector3.one;

            Color newColor = _swordSprite.color;
            newColor.a = 1;
            _swordSprite.color = newColor;
        }
    }
}
