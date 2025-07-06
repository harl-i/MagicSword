using UnityEngine;
using YG;

[RequireComponent(typeof(Animator))]
public class MoveHint : MonoBehaviour
{
    private void OnEnable()
    {
        if (YG2.saves.newGamePlus == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}