using System;
using UnityEngine;

namespace Sword
{
    public class PlatformDestructionSkill : MonoBehaviour
    {
        public static Action<float> SkillActivated;

        public void OnButtonClick(float time)
        {
            SkillActivated?.Invoke(time);
        }
    }
}