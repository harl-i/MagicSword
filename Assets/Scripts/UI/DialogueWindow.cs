using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    public Action WindowShown;

    public void OnWindowShowAnimationFinished()
    {
        WindowShown?.Invoke();
    }
}
