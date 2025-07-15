using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCooldown : MonoBehaviour
{
    [SerializeField] private float _cooldownTime = 5f;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(StartCooldown);
    }

    private void StartCooldown()
    {
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        _button.interactable = false;

        float timer = 0;
        while (timer < _cooldownTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _button.interactable = true;
    }
}
