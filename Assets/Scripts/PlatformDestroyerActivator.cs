using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlatformDestroyerActivator : MonoBehaviour
{
    [SerializeField] private PlatformDestroyer _platformDestroyer;
    [SerializeField] private Button _platformDestroyerButton;
    [SerializeField] private ShieldActivator _shieldActivator;

    private bool _isBlock = false;

    private void OnEnable()
    {
        PlatformDestructionSkill.SkillActivated += OnPlatformDestructionActivated;
    }

    private void OnDisable()
    {
        PlatformDestructionSkill.SkillActivated -= OnPlatformDestructionActivated;
    }

    public void Block()
    {
        _isBlock = true;
        _platformDestroyerButton.enabled = false;
    }

    public void Unblock()
    {
        _isBlock = false;
        _platformDestroyerButton.enabled = true;
    }

    private void OnPlatformDestructionActivated(float time)
    {
        if (!_isBlock)
        {
            StartCoroutine(PlayDestructionAnimation(time));
        }
    }

    private IEnumerator PlayDestructionAnimation(float time)
    {
        _shieldActivator.Block();
        _platformDestroyer.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        _platformDestroyer.gameObject.SetActive(false);
        _shieldActivator.Unblock();
    }
}
