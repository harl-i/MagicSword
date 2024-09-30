using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoveHint : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }
}