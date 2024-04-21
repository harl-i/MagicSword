using System.Collections;
using UnityEngine;

public class PlatformDestroyerActivator : MonoBehaviour
{
    [SerializeField] private PlatformDestroyer _platformDestroyer;

    private void OnEnable()
    {
        PlatformDestructionSkill.SkillActivated += OnPlatformDestructionActivated;
    }

    private void OnDisable()
    {
        PlatformDestructionSkill.SkillActivated -= OnPlatformDestructionActivated;
    }

    private void OnPlatformDestructionActivated(float time)
    {
        StartCoroutine(PlayDestructionAnimation(time));
    }

    private IEnumerator PlayDestructionAnimation(float time)
    {
        _platformDestroyer.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        _platformDestroyer.gameObject.SetActive(false);
    }
}
