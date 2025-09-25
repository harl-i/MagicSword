using UnityEngine;
using YG;

namespace TriggersAndZones
{
    public class NewGamePlusTrigger : MonoBehaviour
    {
        public void Activate()
        {
            YG2.saves.NewGamePlus = 1;
        }
    }
}