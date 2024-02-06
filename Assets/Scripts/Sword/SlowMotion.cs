using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    private float _originalTimeScale;
    private Coroutine _startSlowMotion;

    private void Start()
    {
        _originalTimeScale = Time.timeScale;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startSlowMotion = StartCoroutine(StartSlowMotion());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(_startSlowMotion);
            Time.timeScale = _originalTimeScale;
        }
    }

    private IEnumerator StartSlowMotion()
    {
        float currentTime = 0f;
        float duration = 0.6f;
        float targetTimeScale = 0.3f;

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(_originalTimeScale, targetTimeScale, currentTime / duration);
            yield return null;
        }

        Time.timeScale = targetTimeScale;
    }
}
