using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace UI
{
    public class ContinueMenu : MonoBehaviour
    {
        private const string Id = "continue";
        private const int MaxContinuesAmount = 3;

        [SerializeField] private int _soulsForContinue;
        [SerializeField] private GameObject _error;
        [SerializeField] private int _delay;
        [SerializeField] private GameObject _mainMenu;

        private void OnEnable() => _mainMenu.SetActive(false);

        public void Close()
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        public void ContinueForSouls()
        {
            if (YG2.saves.SoulsCount >= _soulsForContinue)
            {
                YG2.saves.SoulsCount -= _soulsForContinue;
                ResetContinues();
                LoadContinueLevel();
            }
            else
            {
                StartCoroutine(ShowError(_delay));
            }
        }

        public void ContinueForAdvertisment()
        {
            ResetContinues();
            ShowAdvReward();
        }

        private void ShowAdvReward() => YG2.RewardedAdvShow(Id, LoadContinueLevel);

        private void LoadContinueLevel() => SceneManager.LoadScene(YG2.saves.SceneForContinue);

        private void ResetContinues() => YG2.saves.Continues = MaxContinuesAmount;

        private IEnumerator ShowError(int delay)
        {
            _error.SetActive(true);
            yield return new WaitForSeconds(delay);
            _error.SetActive(false);
        }
    }
}
