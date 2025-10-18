using UnityEngine;
using YG;

namespace UI
{
    public class VolumeUpdater : MonoBehaviour
    {
        private void Start()
        {
            YG2.saves.Volume = (int)AudioListener.volume;
        }
    }
}