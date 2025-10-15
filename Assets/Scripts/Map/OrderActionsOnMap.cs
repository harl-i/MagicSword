using System.Collections;
using LevelsManagment;
using UnityEngine;

namespace Map
{
    public class OrderActionsOnMap : MonoBehaviour
    {
        [SerializeField] private SwordIconAnimMover _swordIconAnimMover;
        [SerializeField] private NextSceneLoader _nextSceneLoader;

        private void Start() => StartCoroutine(StartAnimationAndLoadScene());

        private IEnumerator StartAnimationAndLoadScene()
        {
            yield return StartCoroutine(_swordIconAnimMover.StartAnimationAfterDelay());

            yield return new WaitUntil(() => _swordIconAnimMover.IsMoveCompleted);

            _nextSceneLoader.LoadScene();
        }
    }
}
