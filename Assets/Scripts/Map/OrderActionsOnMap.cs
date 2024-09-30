using System.Collections;
using UnityEngine;

public class OrderActionsOnMap : MonoBehaviour
{
    [SerializeField] private SwordIconAnimMover _swordIconAnimMover;
    [SerializeField] private NextSceneLoader _nextSceneLoader;

    private void Start()
    {
        StartCoroutine(StartAnimationAndLoadScene());
    }

    private IEnumerator StartAnimationAndLoadScene()
    {
        yield return StartCoroutine(_swordIconAnimMover.StartAnimationAfterDelay());

        while (!_swordIconAnimMover.IsMoveCompleted)
        {
            yield return null;
        }

        _nextSceneLoader.LoadScene();
    }
}
