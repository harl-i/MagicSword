using UnityEngine;
using YG;

namespace UI
{
    public class GodModeSwitcher : MonoBehaviour
    {
        public void SwitchMode()
        {
            if (YG2.saves.GodMode == 1)
            {
                YG2.saves.GodMode = 0;
            }
            else
            {
                YG2.saves.GodMode = 1;
            }
        }
    }
}