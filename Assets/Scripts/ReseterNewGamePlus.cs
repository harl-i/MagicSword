using UnityEngine;
using YG;

public class ReseterNewGamePlus : MonoBehaviour
{
    public void ResetTrigger()
    {
        YG2.saves.newGamePlus = 0;
    }
}
