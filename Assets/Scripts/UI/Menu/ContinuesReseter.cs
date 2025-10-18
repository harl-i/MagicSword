using UnityEngine;
using YG;

namespace UI
{
    public class ContinuesReseter : MonoBehaviour
    {
        private const int MaxContinuesAmount = 3;

        public void Reset() => YG2.saves.Continues = MaxContinuesAmount;
    }
}
