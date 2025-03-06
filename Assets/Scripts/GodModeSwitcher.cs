using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GodModeSwitcher : MonoBehaviour
{
    public void SwitchMode()
    {
        if (YG2.saves.godMode == 1)
        {
            YG2.saves.godMode = 0;
        } else
        {
            YG2.saves.godMode = 1;
        }

        Debug.Log($"{YG2.saves.godMode}");
    }
}
