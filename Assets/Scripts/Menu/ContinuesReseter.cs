using UnityEngine;
using YG;

public class ContinuesReseter : MonoBehaviour
{
    public void Reset()
    {
        YG2.saves.Continues = 3;
    }
}
