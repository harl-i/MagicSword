using System;
using System.Collections;
using UnityEngine;

public class SoulMover : MonoBehaviour
{
    [SerializeField] private Transform _portal;

    private Soul _soul;

    private void OnEnable()
    {
        Soul.SoulSpawned += HandleSoulSpawned;
    }

    private void OnDisable()
    {
        Soul.SoulSpawned -= HandleSoulSpawned;
    }

    private void HandleSoulSpawned(Soul soul)
    {
        _soul = soul;
    }

    private void Update()
    {
        if (_soul != null)
        {
            StartCoroutine(MoveSoulToPortal(_soul));
        }
    }

    private IEnumerator MoveSoulToPortal(Soul soul)
    {
        float speed = 0.01f; // —корость перемещени€ души
        Vector3 direction = (_portal.position - soul.transform.position).normalized; // Ќаправление движени€
        float distance = Vector3.Distance(soul.transform.position, _portal.position); // –ассто€ние до цели

        while (distance > 0.1f) // ѕровер€ем, достаточно ли близко душа к цели
        {
            //// √енерируем случайное число дл€ смещени€ в пределах [-0.01, 0.01]
            //float randomOffset = UnityEngine.Random.Range(-0.01f, 0.01f);

            //// ѕримен€ем смещение к направлению движени€
            //direction.x += randomOffset;

            // Ќормализуем направление снова, чтобы оно оставалось нормальным
            direction.Normalize();

            soul.transform.position += direction * speed * Time.deltaTime; // ѕеремещаем душу в направлении цели
            distance = Vector3.Distance(soul.transform.position, _portal.position); // ќбновл€ем рассто€ние
            yield return null; // ∆дем следующий кадр
        }

        // ƒуша достигла цели, здесь можно выполнить дополнительные действи€, например, активировать анимацию
    }
}
