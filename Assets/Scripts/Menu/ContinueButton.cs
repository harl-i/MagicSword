using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject _continueMenu;

    public void OpenMenu() 
    {
        _continueMenu.SetActive(true);
    }
}
