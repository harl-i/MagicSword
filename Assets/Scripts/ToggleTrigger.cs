using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ToggleTrigger : MonoBehaviour
{
    private PolygonCollider2D _polygonCollider;

    private void OnEnable()
    {
        PlatformDestructionSkill.SkillActivated += Toggle;
    }

    private void OnDisable()
    {
        PlatformDestructionSkill.SkillActivated -= Toggle;
    }

    private void Awake()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public void Toggle(float timer)
    {
        SwitchToTrigger();
        StartCoroutine(SwitchToCollider(timer));
    }

    private void SwitchToTrigger()
    {
        _polygonCollider.isTrigger = true;
    }

    private IEnumerator SwitchToCollider(float switchDelay)
    {
        yield return new WaitForSeconds(switchDelay);

        _polygonCollider.isTrigger = false;
    }
}
