using UnityEngine;
using YG;

public class ReseterLevelsFlags : MonoBehaviour
{
    public void ResetFlags()
    {
        YG2.saves.CutScene1Watched = 0;
        YG2.saves.CutScene2Watched = 0;
        YG2.saves.CutScene3Watched = 0;
        YG2.saves.CutScene4Watched = 0;
        YG2.saves.CutScene5Watched = 0;
        YG2.saves.CutScene6Watched = 0;
        YG2.saves.CutScene7Watched = 0;
        YG2.saves.CutScene8Watched = 0;
        YG2.saves.CutScene9Watched = 0;
        YG2.saves.CutScene10Watched = 0;
        YG2.saves.CutScene11Watched = 0;
        YG2.saves.CutScene12Watched = 0;
        YG2.saves.CutScene13Watched = 0;
        YG2.saves.CutScene14Watched = 0;

        YG2.saves.FirstLevelDialogueWatch = 0;
        YG2.saves.ThirdLevelDialogueWatch = 0;
        YG2.saves.FifthLevelDialogueWatch = 0;
        YG2.saves.SeventhLevelDialogueWatch = 0;
    }
}
