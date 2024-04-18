using System;
using UnityEngine;

public class PlatformDestructionSkill : MonoBehaviour
{
    public static Action<float> SkillActivated;

    public void OnButtonClick(float time)
    {
        SkillActivated?.Invoke(time);
    }
}
