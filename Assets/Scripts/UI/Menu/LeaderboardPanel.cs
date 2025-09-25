using UnityEngine;

namespace UI
{
    public class LeaderboardPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;

        public void Close()
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}