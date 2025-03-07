using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levelChangeMenu;

    public void EnableLevelsMenu()
    {
        _levelChangeMenu.gameObject.SetActive(true);
    }
}
