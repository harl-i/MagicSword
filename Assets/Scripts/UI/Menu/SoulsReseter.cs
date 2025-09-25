using TMPro;
using UnityEngine;
using YG;

public class SoulsReseter : MonoBehaviour
{
    [SerializeField] private TMP_Text _soulsCountUI;

    public void Reset()
    {
        YG2.saves.SoulsCount = 0;
        YG2.SetLeaderboard("soulsCountLeaderboard", 1);
        _soulsCountUI.text = YG2.saves.SoulsCount.ToString();
    }
}
