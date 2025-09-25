using UnityEngine;
using YG;

namespace UI
{
    public class ContinuesReseter : MonoBehaviour
    {
        public void Reset()
        {
            YG2.saves.Continues = 3;
        }
    }
}
