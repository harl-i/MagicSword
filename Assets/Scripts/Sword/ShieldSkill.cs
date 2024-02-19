using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : MonoBehaviour
{
    public static Action<float> SkillActivated;

    public void OnButtonClick(float time)
    {
        SkillActivated?.Invoke(time);
    }
}
