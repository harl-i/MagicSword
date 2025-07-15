using UnityEngine;
using YG;

public class ReseterNewGamePlus : MonoBehaviour
{
    [SerializeField] private GameObject _newGame;
    [SerializeField] private GameObject _newGamePlus;

    public void ResetTrigger()
    {
        YG2.saves.newGamePlus = 0;
        _newGame.SetActive(true);
        _newGamePlus.SetActive(false);
    }
}
