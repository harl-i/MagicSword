using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderActionsOnMap : MonoBehaviour
{
    [SerializeField] private SwordIconAnimMover _swordIconAnimMover;
    [SerializeField] private LoadSceneByIndex _loadSceneByIndex;

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

        yield return StartCoroutine(_loadSceneByIndex.LoadScene());
    }
}
