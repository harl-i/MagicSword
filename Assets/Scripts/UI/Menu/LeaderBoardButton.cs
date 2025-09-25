using UnityEngine;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _mainMenu;

    public void Open()
    {
        _leaderboardPanel.SetActive(true);
        _mainMenu.SetActive(false);
    }
}
