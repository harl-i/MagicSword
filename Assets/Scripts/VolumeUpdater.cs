using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class VolumeUpdater : MonoBehaviour
{
    private void Start()
    {
        YG2.saves.volume = (int)AudioListener.volume;
    }
}
