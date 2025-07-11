using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldActivator : MonoBehaviour
{
    [SerializeField] private Shield _shield;
    [SerializeField] private Button _shieldButtonMobile;
    [SerializeField] private Button _shieldButtonDesktop;
    [SerializeField] private PlatformDestroyerActivator _platformDestroyerActivator;
    [SerializeField] private TextMeshProUGUI _shieldCounterDesktop;
    [SerializeField] private TextMeshProUGUI _shieldCounterMobile;

    private bool _isBlock = false;
    private bool _isActive = false;
    private int _activationCount = 3;

    public Action<bool> ShieldActivated;

    private void Start()
    {
        _shieldCounterDesktop.text = _activationCount.ToString();
        _shieldCounterMobile.text = _activationCount.ToString();
    }

    private void OnEnable()
    {
        ShieldSkill.SkillActivated += OnSkillActivated;
    }

    private void OnDisable()
    {
        ShieldSkill.SkillActivated -= OnSkillActivated;
    }

    public void Block()
    {
        _isBlock = true;
        _shieldButtonMobile.enabled = false;
        _shieldButtonDesktop.enabled = false;
    }

    public void Unblock()
    {
        _isBlock = false;
        _shieldButtonMobile.enabled = true;
        _shieldButtonDesktop.enabled = true;
    }

    private void OnSkillActivated(float time)
    {
        if (!_isBlock && _activationCount > 0 && !_isActive)
        {
            StartCoroutine(TemporarilyEnable(time));
        }
    }

    private IEnumerator TemporarilyEnable(float time)
    {
        _isActive = true;
        _platformDestroyerActivator.Block();
        _shield.gameObject.SetActive(true);
        ShieldActivated?.Invoke(true);

        _activationCount--;
        _shieldCounterDesktop.text = _activationCount.ToString();
        _shieldCounterMobile.text = _activationCount.ToString();

        yield return new WaitForSeconds(time);

        _shield.gameObject.SetActive(false);
        ShieldActivated?.Invoke(false);
        _platformDestroyerActivator.Unblock();
        _isActive = false;
    }
}
