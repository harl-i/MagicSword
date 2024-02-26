using System;
using System.Collections;
using UnityEngine;

public class ShieldActivator : MonoBehaviour
{
    [SerializeField] private Shield _shield;

    public Action<bool> ShieldActivated;

    private void OnEnable()
    {
        ShieldSkill.SkillActivated += OnSkillActivated;
    }

    private void OnDisable()
    {
        ShieldSkill.SkillActivated -= OnSkillActivated;
    }

    private void OnSkillActivated(float time)
    {
        StartCoroutine(TemporarilyEnable(time));
    }

    private IEnumerator TemporarilyEnable(float time)
    {
        _shield.gameObject.SetActive(true);
        ShieldActivated?.Invoke(true);

        yield return new WaitForSeconds(time);

        _shield.gameObject.SetActive(false);
        ShieldActivated?.Invoke(false);
    }
}
