using UnityEngine;
using YG;

public class NewGamePlusTrigger : MonoBehaviour
{
    public void Activate()
    {
        YG2.saves.newGamePlus = 1;
    }
}
