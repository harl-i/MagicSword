using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ContinueButton : MonoBehaviour
{
    public void LoadContinueLevel() 
    {
        SceneManager.LoadScene(YG2.saves.sceneForContinue);
    }
}
